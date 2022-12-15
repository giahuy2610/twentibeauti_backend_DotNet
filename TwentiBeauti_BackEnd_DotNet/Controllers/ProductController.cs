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


        //done-huy
        [HttpGet("show/{IDProduct:int}")]
        public async Task<Object> show(int IDProduct)
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
    }
}
    