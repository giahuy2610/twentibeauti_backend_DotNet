using Microsoft.AspNetCore.Mvc;
using TwentiBeauti_BackEnd_DotNet.Models;
using TwentiBeauti_BackEnd_DotNet.Services;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class EmailController : Controller
    {

        private readonly IMailService mailService;
        public EmailController(IMailService mailService)
        {
            this.mailService = mailService;
        }

        [HttpPost("Send")]
        public async Task<IActionResult> Send([FromForm] MailRequest request)
        {
            try
            {
                await mailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
