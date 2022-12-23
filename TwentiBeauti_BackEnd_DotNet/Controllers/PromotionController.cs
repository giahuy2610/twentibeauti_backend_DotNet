using Microsoft.AspNetCore.Mvc;
using TwentiBeauti_BackEnd_DotNet.Data;
using TwentiBeauti_BackEnd_DotNet.Models;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [Route("api/promotion")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly Context dbContext;
        public PromotionController(Context dbContext)
        {
            this.dbContext = dbContext;
        }

        // GET: api/<PromotionController>
        [HttpGet("index")]
        public IActionResult Index()
        {
            return Ok(JsonConvert.SerializeObject(dbContext.PromotionRegister.ToList()));
        }

        // GET api/<PromotionController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<PromotionController>
        [HttpPost("create")]
        public IActionResult Create([FromBody] string email)
        {
            var temp = dbContext.PromotionRegister.Where(p => p.Email == email).FirstOrDefault();
            if (temp == null)
            {
                var newPro = new PromotionRegister() { Email = email };
                var cus = dbContext.Customer.Where(c => c.Email == email).FirstOrDefault();
                if (cus != null) newPro.IDCus = cus.IDCus;
                dbContext.PromotionRegister.Add(newPro);
                dbContext.SaveChanges();
                return Ok("Completed");
            }
            return Ok("Is exist");
        }

        // PUT api/<PromotionController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<PromotionController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        [HttpPost("send")]
        public void SendEmail(dynamic request)
        {
            foreach(var temp in request["Customers"])
            {
                new EmailController().SendEmail(temp["Email"].ToString(),"Chương trình khuyến mãi của TWENTI", request["Content"].ToString());
            }
        }
    }
}
