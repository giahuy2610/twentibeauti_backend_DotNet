using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using TwentiBeauti_BackEnd_DotNet.Data;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/tag")]
    public class TagController : Controller
    {
        private readonly Context dbContext;
        public TagController(Context dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("index/{IDType}")]
        public async Task<IActionResult> index(int IDType)
        {
            return Ok(JsonConvert.SerializeObject(await dbContext.Tag.Where(r => r.IDType == IDType).ToListAsync()));
        }
    }
}
