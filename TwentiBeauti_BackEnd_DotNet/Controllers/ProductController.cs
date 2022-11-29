using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwentiBeauti_BackEnd_DotNet.Data;
using TwentiBeauti_BackEnd_DotNet.Models;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly Context dbContextProduct;
        public ProductController(Context dbContextProduct)
        {
            this.dbContextProduct = dbContextProduct;
        }
        [HttpPut]
        [Route("update/{IDProduct:int}")]
        public async Task<IActionResult> UpdateStockProduct([FromRoute] int IDProduct, Product updateProductRequest)
        {
            var invoiceDetail = await dbContextProduct.Products.FindAsync(IDProduct);

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
    