using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TwentiBeauti_BackEnd_DotNet.Data;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/image-slider")]
    public class ImageSliderController : Controller
    {
        private readonly Context dbContext;
        public ImageSliderController(Context dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("available")]//done
        public async Task<IActionResult> available()
        {
            return Ok(JsonConvert.SerializeObject(dbContext.ImageSlider.Where(c => c.StartOn <= DateTime.Now && c.EndOn >= DateTime.Now &&  c.IsDeleted == false)));
        }
    }
}
