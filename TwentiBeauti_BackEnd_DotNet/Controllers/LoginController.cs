using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TwentiBeauti_BackEnd_DotNet.Data;
using TwentiBeauti_BackEnd_DotNet.Models;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/")]
    public class LoginController : Controller
    {
        private readonly Context dbContext;
        public LoginController(Context dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] dynamic request)
        {
            var uid = "";
            uid = request["user"]["uid"] ;
            var email = request["user"]["email"];
            
            // check exist -> get user info , if not -> create
            if (dbContext.Customer.Where(c => c.UID == uid).Any())
            {
                return Ok(JsonConvert.SerializeObject(dbContext.Customer.Where(c => c.UID == uid).FirstOrDefault()));
            }
            else
            {
                var cus = new Customer();
                cus.UID = uid;
                cus.Email = email;
                await dbContext.Customer.AddAsync(cus);
                await dbContext.SaveChangesAsync();
                return Ok(Ok(JsonConvert.SerializeObject(cus)));
            }


        }
    }
}
