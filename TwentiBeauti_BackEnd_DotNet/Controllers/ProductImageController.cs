using Microsoft.AspNetCore.Mvc;
using TwentiBeauti_BackEnd_DotNet.Data;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/product-image")]
    public class ProductImageController : Controller
    {
        private readonly Context dbContext;
        public ProductImageController(Context dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("index/{IDProduct}")]
        public async Task<IActionResult> index(int IDProduct)
        {
            return Ok(dbContext.ProductImage.Where(r => r.IDProduct == IDProduct));
        }
    }
}
