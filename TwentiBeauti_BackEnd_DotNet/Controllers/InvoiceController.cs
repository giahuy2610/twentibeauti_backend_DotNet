using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Dynamic;
using TwentiBeauti_BackEnd_DotNet.Data;
using TwentiBeauti_BackEnd_DotNet.Models;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/invoice")]
    public class InvoiceController : ControllerBase
    {
        private readonly Context dbContextInvoice;
        public InvoiceController(Context dbContextInvoice)
        {
            this.dbContextInvoice = dbContextInvoice;
        }


        [HttpGet("show/{IDInvoice:int?}")]//pending
        public async Task<IActionResult> GetInvoice([FromRoute] int IDInvoice = 0)
        {
            if (IDInvoice == 0) 
                return Ok(JsonConvert.SerializeObject(dbContextInvoice.Invoice.ToList()));

            var invoice = dbContextInvoice.Invoice.Find(IDInvoice);
            if (invoice == null) return NotFound();

            var productsList = dbContextInvoice.InvoiceDetail.Where(c => c.IDInvoice == IDInvoice).ToList();

            List<dynamic> products = new List<dynamic>();

            foreach(var product in productsList)
            {
                products.Add(await new ProductController(dbContextInvoice).showByTime(product.IDProduct, invoice.CreatedOn));
            }

            var address = dbContextInvoice.Address.Find(invoice.IDAddress);
            var coupon = dbContextInvoice.Coupon.Find(invoice.IDCoupon);

            var json = JsonConvert.SerializeObject(invoice);
            dynamic data = JsonConvert.DeserializeObject(json, typeof(ExpandoObject));
            data.Address = address;
            data.Coupon = coupon;
            data.Products = products;
            for (var i = 0; i < productsList.Count; i++)
            {
                data.Products[i].Quantity = productsList[i].Quantity;
            }
            return Ok(JsonConvert.SerializeObject(data));
        }

        [HttpGet]
        [Route("customer/{IDCus:int}")]//done
        public async Task<IActionResult> GetInvoiceOfCustomer([FromRoute] int IDCus)
        {
            //get list of his invoice
            var invoiceOfCus = dbContextInvoice.Invoice.Where(
                row => row.IDCus == IDCus
                ).ToList();
            
            List<dynamic>? listOfInvoice = new List<dynamic> { };

            foreach (var invoice in invoiceOfCus)
            {
                var json = JsonConvert.SerializeObject(invoice);
                dynamic data = JsonConvert.DeserializeObject(json, typeof(ExpandoObject));
                var address = dbContextInvoice.Address.Find(invoice.IDAddress);
                data.Address = address;
                listOfInvoice.Add(data);
            }
            return Ok(JsonConvert.SerializeObject(listOfInvoice));
        }

        [HttpPut("tracking-status")] //done
        public async Task<IActionResult> changeTracking([FromBody] dynamic a)
        {
           var invoice = dbContextInvoice.Invoice.Find((int)a["IDInvoice"]);
            invoice.IDTracking = a["IDTracking"];
            dbContextInvoice.SaveChanges();
            return Ok(invoice);
        }

        [HttpGet("vnpay-return")] // done
        public async Task<IActionResult> PaymentCallback([FromQuery]string vnp_ResponseCode, [FromQuery] int vnp_TxnRef)
        {
            if (vnp_ResponseCode == "00")
            {
                //check if paid => update invoice ispaid column to true 
                var invoice = dbContextInvoice.Invoice.Find(vnp_TxnRef);
                invoice.IsPaid = true;
                dbContextInvoice.SaveChanges();
                return Redirect("http://localhost:3000/details/"+ vnp_TxnRef.ToString());
            }
            else
            {
                var invoice = dbContextInvoice.Invoice.Find(vnp_TxnRef);
                invoice.IsPaid = false;
                invoice.MethodPay = 1;
                dbContextInvoice.SaveChanges();
                return Redirect("http://localhost:3000/details/" + vnp_TxnRef.ToString());
            }
        }


        [HttpPost("create")]
        public async Task<IActionResult> createInvoice(dynamic request)
        {
            try
            {
                var invoice = new Invoice();
                invoice.IDCus = request["IDCus"];
                invoice.MethodPay = request["MethodPay"];
                invoice.IDTracking = 1;
                invoice.CreatedOn = DateTime.Now;
                var codeCoupon = "";
                    codeCoupon = request["CodeCoupon"];
                if (codeCoupon != null && codeCoupon != "")
                {
                    var coupon = dbContextInvoice.Coupon.Where(c => c.CodeCoupon == codeCoupon).FirstOrDefault();
                    if (coupon == null) return BadRequest("Mã giảm giá không tồn tại");
                    else if (coupon.Stock == 0 || coupon.StartOn > DateTime.Now || coupon.EndOn < DateTime.Now)
                        return BadRequest("Not available now (out of stock, expired..etc...");
                    else
                    {
                        coupon.Stock -= 1;
                        invoice.IDCoupon = coupon.IDCoupon;
                        invoice.TotalValue = coupon.ValueDiscount;
                    }
                }
                
                foreach (var product in request["InvoiceDetail"])
                {
                    var productStock = dbContextInvoice.Product.Find((int)product["IDProduct"]).Stock;
                    if (productStock - (int)product["Quantity"] < 0) return BadRequest("Hết hàng");
                }

                //create address
                var address = new Address()
                {
                    City = request["City"],
                    District = request["District"],
                    Ward = request["Ward"],
                    AddressDetail = request["AddressDetail"],
                    Email = request["Email"],
                    Phone = request["Phone"],
                    FirstName = request["FirstName"],
                    LastName = request["LastName"],
                };

                dbContextInvoice.Address.Add(address);
                dbContextInvoice.SaveChanges();

                //create a new invoice record
                invoice.IDAddress = address.IDAddress;
                dbContextInvoice.Invoice.Add(invoice);
                dbContextInvoice.SaveChanges();

                //create a list of invoiceDetail
                foreach (var product in request["InvoiceDetail"])
                {
                    var invoiceDetail = new InvoiceDetail()
                    {
                        IDInvoice = invoice.IDInvoice,
                        IDProduct = product["IDProduct"],
                        Quantity = product["Quantity"]
                    };
                    dbContextInvoice.InvoiceDetail.Add(invoiceDetail);
                    dbContextInvoice.SaveChanges();
                }

                //minus quantity of products
                var totalValue = 0;
                foreach (var product in request["InvoiceDetail"])
                {
                    var productDetail = dbContextInvoice.Product.Find((int)product["IDProduct"]);
                    productDetail.Stock -= (int)product["Quantity"];
                    dbContextInvoice.SaveChanges();
                    var id = (int) product["IDProduct"];
                    totalValue += (new RetailPriceController(dbContextInvoice).showCurrent(id))*((int)product["Quantity"]);
                }
                invoice.TotalValue = totalValue;
                dbContextInvoice.Cart.RemoveRange(dbContextInvoice.Cart.Where(c => c.IDCus == invoice.IDCus));
                dbContextInvoice.SaveChanges();
                if (invoice.MethodPay == 1)
                {
                    new EmailController().SendEmail(address.Email,"Xác nhận đơn hàng", "Cảm ơn bạn đã mua hàng tại TWENTI. Theo dõi đơn hàng tại http://192.168.1.2:3000/details/"+invoice.IDInvoice);
                }
                return Ok(JsonConvert.SerializeObject(invoice));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
            
        }


    }

}

//        //[HttpPost]
//        //[Route("invoice/create")]
//        //public async Task<IActionResult> CreateInvoice()
//        //{
//        //    //var coupon = this.dbContextInvoice.CoupGetCoupon();
//        //    var newInvoice = new Invoice();
//        //    this.dbContextInvoice.Invoice.Add(newInvoice);
//        //    return Ok();
//        //}

////public async Task<IActionResult> CreateInvocie1()
////{
////    return Ok();
////}
//        //[HttpGet]
//        //[Route("get/{IDCus}")]
//        //public async Task<IActionResult> GetAddress([FromRoute] int IDCus)
//        //{
//        //    return Ok(this.dbContextInvoice.Invoice.Where(Invoice => Invoice.IDCus == IDCus).First().IDInvoice);

//        //    //        var invoice = await dbContextInvoice.Invoice.FindAsync(IDCus);


//        //    //    if (invoice == null)
//        //    //    {
//        //    //        return NotFound();
//        //    //    }
//        //    //    return Ok(invoice);

//        //    //}

//        //}
//    }
//}
