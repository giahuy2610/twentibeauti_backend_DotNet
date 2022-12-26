using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Dynamic;
using TwentiBeauti_BackEnd_DotNet.Data;
using TwentiBeauti_BackEnd_DotNet.Models;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/product")]
    public class ProductController : ControllerBase
    {
        private readonly Context dbContextProduct;

        public ProductController(Context dbContextProduct)
        {
            this.dbContextProduct = dbContextProduct;
        }

        [HttpGet("index")]
        public async Task<IActionResult> index()
        {
            var products = await dbContextProduct.Product.ToListAsync();
            List<dynamic> data = new List<dynamic>();
            foreach (var product in products)
            {
                data.Add(await show(product.IDProduct));
            }
            return Ok(JsonConvert.SerializeObject(data));
        }

        //done-huy
        [HttpGet("show/{IDProduct:int}")]
        public async Task<IActionResult> showSpecified(int? IDProduct)
        {
            var data = await show(IDProduct);
            return Ok(JsonConvert.SerializeObject(data));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<Object> show(int? IDProduct)
        {
            //get product
            var product = await dbContextProduct.Product.FindAsync(IDProduct);
            var json = JsonConvert.SerializeObject(product);
            dynamic a = JsonConvert.DeserializeObject(json, typeof(ExpandoObject));
            
            var brand = await dbContextProduct.Brand.FindAsync(product.IDBrand);
            a.Brand = brand;

            var images = dbContextProduct.ProductImage.Where(i => i.IDProduct == IDProduct).Select(i => new { i.Path });
            a.Images = images;

            var reviews = dbContextProduct.Review.Where(i => i.IDProduct == IDProduct);
            a.Reviews = reviews;
            a.Rating = !reviews.Any() ? 0 : reviews.Average(r => r.Rating);

            a.RetailPrice = new RetailPriceController(dbContextProduct).showCurrent(IDProduct);
            
            return (a);
        }


        [HttpPut]
        [Route("update/{IDProduct:int}")]
        public async Task<IActionResult> UpdateStockProduct([FromRoute] int IDProduct, Product updateProductRequest)
        {
            var invoiceDetail = await dbContextProduct.Product.FindAsync(IDProduct);

            if (invoiceDetail != null)
            {
                invoiceDetail.Stock = updateProductRequest.Stock;

                await dbContextProduct.SaveChangesAsync();
                return Ok(invoiceDetail);

            }
            return NotFound();


        }
        
        [HttpGet("search/{key}")]
        public async Task<IActionResult> Search(string key)
        {

            var productsInCol = dbContextProduct.Product.Where(c => c.IsDeleted == false && c.Stock > 0 && c.NameProduct.Contains(key)).ToList();
            List<dynamic> products = new List<dynamic>();
            foreach (var product in productsInCol)
            {
                var productDetail = await new ProductController(dbContextProduct).show(product.IDProduct);
                products.Add(productDetail);
            }
            return Ok(JsonConvert.SerializeObject(products));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(dynamic request)
        {
            try
            {
                var product = new Product();
                product.NameProduct = request["NameProduct"];
                product.IDBrand = request["IDBrand"];
                product.Description = request["Description"];
                product.Stock = request["Stock"];
                product.Mass = request["Mass"];
                product.UnitsOfMass = request["UnitsOfMass"];
                product.Units = request["Units"];
                product.ApplyTaxes = request["ApplyTaxes"];
                product.StatusSale = request["StatusSale"];
                product.ListPrice = request["ListPrice"];
                product.IDType = request["IDType"];
                product.IDTag = request["IDTag"];
                product.CreatedOn = DateTime.Now;
                dbContextProduct.Product.Add(product);
                dbContextProduct.SaveChanges();

                var products = request["Images"];
                foreach (var path in products)
                {
                    var img = new ProductImage()
                    {
                        IDProduct = (int) product.IDProduct,
                        Path = path
                    };
                    dbContextProduct.ProductImage.Add(img);
                    dbContextProduct.SaveChanges();
                }

                return Ok(JsonConvert.SerializeObject(await show(product.IDProduct)));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }




        }


        [HttpPost("update")]
        public async Task<IActionResult> Update(dynamic request)
        {
            try
            {
                var product = dbContextProduct.Product.Find((int)request["IDProduct"]);
                product.NameProduct = request["NameProduct"];
                product.IDBrand = request["IDBrand"];
                product.Description = request["Description"];
                product.Stock = request["Stock"];
                product.Mass = request["Mass"];
                product.UnitsOfMass = request["UnitsOfMass"];
                product.Units = request["Units"];
                product.ApplyTaxes = request["ApplyTaxes"];
                product.StatusSale = request["StatusSale"];
                product.ListPrice = request["ListPrice"];
                product.IDType = request["IDType"];
                product.IDTag = request["IDTag"];
                product.CreatedOn = product.CreatedOn;
                dbContextProduct.SaveChanges();

                var id = 0;
                    id= request["IDProduct"];
                dbContextProduct.ProductImage.RemoveRange(dbContextProduct.ProductImage.Where(i => i.IDProduct == id).ToList());

                var products = request["Images"];
                foreach (var path in products)
                {
                    var img = new ProductImage()
                    {
                        IDProduct = id,
                        Path = path
                    };
                    dbContextProduct.ProductImage.Add(img);
                    dbContextProduct.SaveChanges();
                }

                return Ok(JsonConvert.SerializeObject(await show(product.IDProduct)));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<Object> showByTime(int? IDProduct, DateTime timeMark)
        {
            //get product
            var product = await dbContextProduct.Product.FindAsync(IDProduct);
            var json = JsonConvert.SerializeObject(product);
            dynamic a = JsonConvert.DeserializeObject(json, typeof(ExpandoObject));

            var brand = await dbContextProduct.Brand.FindAsync(product.IDBrand);
            a.Brand = brand;

            var images = dbContextProduct.ProductImage.Where(i => i.IDProduct == IDProduct).Select(i => new { i.Path });
            a.Images = images;

            var reviews = dbContextProduct.Review.Where(i => i.IDProduct == IDProduct);
            a.Reviews = reviews;
            a.Rating = !reviews.Any() ? 0 : reviews.Average(r => r.Rating);

            a.RetailPrice = new RetailPriceController(dbContextProduct).showByTime(IDProduct, timeMark);

            return (a);
        }


    }
}
    