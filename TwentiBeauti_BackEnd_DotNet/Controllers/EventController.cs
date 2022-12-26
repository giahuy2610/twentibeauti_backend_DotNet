using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Dynamic;
using TwentiBeauti_BackEnd_DotNet.Data;
using TwentiBeauti_BackEnd_DotNet.Models;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/event")]
    public class EventController : Controller
    {
        private readonly Context dbContext;
        public EventController(Context dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("index")]
        public async Task<IActionResult> index()
        {
            return Ok(JsonConvert.SerializeObject(dbContext.Event.ToList()));
        }

        [HttpGet("show/{IDEvent}")]
        public async Task<IActionResult> get(int IDEvent)
        {
            var eventDetail = dbContext.Event.Find(IDEvent);

            var json = JsonConvert.SerializeObject(eventDetail);
            dynamic data = JsonConvert.DeserializeObject(json, typeof(ExpandoObject));
            data.Products = dbContext.RetailPrice.Where(p => p.IDEvent == IDEvent).Distinct().ToList();
            return Ok(JsonConvert.SerializeObject(data));
        }

        [HttpPost("create")]
        public async Task<IActionResult> create(dynamic request)
        {
            try
            {
                var eventDetail = new Event()
                {
                    NameEvent = request.NameEvent,
                    ValueDiscount = request.ValueDiscount,
                    UnitsDiscount = request.UnitsDiscount,
                    StartOn = request.StartOn,
                    EndOn = request.EndOn,
                    CreatedOn = DateTime.Now
                };

                dbContext.Event.Add(eventDetail);
                dbContext.SaveChanges();

                if (request["Products"].Count > 0)
                //create product retail price belonged to this event
                foreach (var product in request["Products"])
                {
         
                        var retail = new RetailPrice()
                        {
                            IDProduct = product,
                            IDEvent = (int)eventDetail.IDEvent,
                            Price = dbContext.Product.Find((int)product).ListPrice,
                            StartOn = request.StartOn,
                            EndOn = request.EndOn,
                            CreatedOn = DateTime.Now
                        };
                        if (request.UnitsDiscount == 1 )
                        {
                            retail.Price -= eventDetail.ValueDiscount;
                        }
                        else if (request.UnitsDiscount == 2)
                        {
                            retail.Price -= eventDetail.ValueDiscount*retail.Price;
                        }
                        else
                        {
                            retail.Price = eventDetail.ValueDiscount;
                        }
                        dbContext.RetailPrice.Add(retail);
                        dbContext.SaveChanges();
                }

                return Ok(JsonConvert.SerializeObject(eventDetail));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }


        [HttpPut("update")]
        public async Task<IActionResult> update(dynamic request)
        {
            try
            {
                var eventDetail = dbContext.Event.Find((int)request["IDEvent"]);

                eventDetail.NameEvent = request.NameEvent;
                eventDetail.ValueDiscount = request.ValueDiscount;
                eventDetail.UnitsDiscount = request.UnitsDiscount;
                eventDetail.StartOn = request.StartOn;
                eventDetail.EndOn = request.EndOn;

                dbContext.SaveChanges();

                dbContext.RetailPrice.RemoveRange(dbContext.RetailPrice.Where(r => r.IDEvent == eventDetail.IDEvent).ToList());

                if (request["Products"].Count > 0)
                    //create product retail price belonged to this event
                    foreach (var product in request["Products"])
                    {

                        var retail = new RetailPrice()
                        {
                            IDProduct = product,
                            IDEvent = (int)eventDetail.IDEvent,
                            Price = dbContext.Product.Find((int)product).ListPrice,
                            StartOn = request.StartOn,
                            EndOn = request.EndOn,
                            CreatedOn = DateTime.Now
                        };
                        if (request.UnitsDiscount == 1)
                        {
                            retail.Price -= eventDetail.ValueDiscount;
                        }
                        else if (request.UnitsDiscount == 2)
                        {
                            retail.Price -= eventDetail.ValueDiscount * retail.Price;
                        }
                        else
                        {
                            retail.Price = eventDetail.ValueDiscount;
                        }
                        dbContext.RetailPrice.Add(retail);
                        dbContext.SaveChanges();
                    }

                return Ok(JsonConvert.SerializeObject(eventDetail));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}
