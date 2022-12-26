using Microsoft.AspNetCore.Mvc;
using TwentiBeauti_BackEnd_DotNet.Data;
using TwentiBeauti_BackEnd_DotNet.Models;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/retail-price")]
    public class RetailPriceController : Controller
    {
        private readonly Context dbContext;

        public RetailPriceController(Context dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("show-current/{IDProduct:int}")] // done
        public int showCurrent(int? IDProduct)
        {
            var retailPrice = dbContext.RetailPrice.Where(r => r.IDProduct == IDProduct && r.StartOn <= DateTime.Now && r.EndOn >= DateTime.Now).OrderBy(r => r.CreatedOn);
            return !retailPrice.Any() ? dbContext.Product.Find(IDProduct).ListPrice : retailPrice.Last().Price;
        }

        [ApiExplorerSettings(IgnoreApi = true)] // done
        public int showByTime(int? IDProduct, DateTime timeMark)
        {
            var retailPrice = dbContext.RetailPrice.Where(r => r.IDProduct == IDProduct && r.StartOn <= timeMark && r.EndOn >= timeMark && r.CreatedOn < timeMark).OrderBy(r => r.CreatedOn);
            return !retailPrice.Any() ? dbContext.Product.Find(IDProduct).ListPrice : retailPrice.Last().Price;
        }
    }
}
