using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.ComponentModel;
using TwentiBeauti_BackEnd_DotNet.Data;
using TwentiBeauti_BackEnd_DotNet.Models;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceController : ControllerBase
    {
        private readonly Context dbContextInvoice;
        public InvoiceController(Context dbContextInvoice)
        {
            this.dbContextInvoice = dbContextInvoice;
        }
        [HttpGet("get")]
        public async Task<IActionResult> GetInvoice()
        {
            return Ok(await dbContextInvoice.Invoice.ToListAsync());

        }
        [HttpGet]
        [Route("get/{IDCus:int}")]
        public async Task<IActionResult> GetInvoiceOfCustomer([FromRoute] int IDCus)
        {
            //get list of his invoice
            var invoiceOfCus = dbContextInvoice.Invoice.Where(
                row => row.IDCus == IDCus
                ).ToList();
            
            List<InvoiceInfo>? listOfInvoice = new List<InvoiceInfo> { };

            foreach (var invoice in invoiceOfCus)
            {
                var invoiceDetail = dbContextInvoice.InvoiceDetail.Where(
                row => row.IDInvoice == invoice.IDInvoice
                ).ToList();

                var address = dbContextInvoice.Address.Find(invoice.IDAddress);

                listOfInvoice.Add(new InvoiceInfo(invoice, address));
            }
            return Ok(listOfInvoice);
        }



        [HttpGet("vnpay-return")]
        public async Task<IActionResult> PaymentCallback()
        {
            if (Request.Query["vnp_ResponseCode"][0] == "00")
            {
                //check if paid => update invoice ispaid column to true 
                var invoice = dbContextInvoice.Invoice.Find(Request.Query["vnp_TxnRef"][0]);
                invoice.IsPaid = true;
                dbContextInvoice.SaveChanges();
                return Ok();
            }

            return Ok(Request.Query["vnp_ResponseCode"][0]);
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
