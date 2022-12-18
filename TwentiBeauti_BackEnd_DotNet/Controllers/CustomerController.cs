using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Dynamic;
using TwentiBeauti_BackEnd_DotNet.Data;
using TwentiBeauti_BackEnd_DotNet.Models;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/customer")]

    public class CustomerController : ControllerBase
    {
        private readonly Context dbContext;
        public CustomerController(Context dbContext)
        {
            this.dbContext = dbContext;
        }


        

        [HttpGet]
        [Route("show/{IDCus:int}")]//done
        public async Task<IActionResult> GetCustomer(int IDCus)
        {
            var cus = await dbContext.Customer.FindAsync(IDCus);
            if (cus == null)
            {
                return NotFound();
            }
            var json = JsonConvert.SerializeObject(cus);
            dynamic data = JsonConvert.DeserializeObject(json, typeof(ExpandoObject));
            data.Phone = "0"+cus.Phone.ToString();
            return Ok(JsonConvert.SerializeObject(data));
        }

        [HttpPost("create")]
        public async Task<IActionResult> AddCustomer(Customer addCustomerRequest)
        {
            var cus = new Customer()
            {
                IDCus = addCustomerRequest.IDCus,
                FirstName = addCustomerRequest.FirstName,
                LastName = addCustomerRequest.LastName,
                Email = addCustomerRequest.Email,
                Phone = addCustomerRequest.Phone,
                IsDeleted = addCustomerRequest.IsDeleted,
            };
            await dbContext.Customer.AddAsync(cus);
            await dbContext.SaveChangesAsync();
            return Ok(JsonConvert.SerializeObject(cus));
        }

        [HttpPut]
        [Route("update/{IDCus:int}")]
        public async Task<IActionResult> UpdateCustomer([FromRoute] int IDCus, Customer updateCustomerRequest)
        {
            var cus = await dbContext.Customer.FindAsync(IDCus);

            if (cus != null)
            {

                cus.FirstName = updateCustomerRequest.FirstName;
                cus.LastName = updateCustomerRequest.LastName;
                cus.Phone = updateCustomerRequest.Phone;
                cus.Email = updateCustomerRequest.Email;
                cus.IsDeleted = updateCustomerRequest.IsDeleted;

                await dbContext.SaveChangesAsync();
                return Ok(JsonConvert.SerializeObject(cus));

            }
            return NotFound();

        }

        [HttpDelete]
        [Route("delete/{IDCus:int}")]
        public async Task<IActionResult> DeleteCustomer([FromRoute] int IDCus)
        {
            var cus = await dbContext.Customer.FindAsync(IDCus);

            if (cus != null)
            {
                dbContext.Remove(cus);
                await dbContext.SaveChangesAsync();
                return Ok(JsonConvert.SerializeObject(cus));

            }
            return NotFound();
        }

    }
}
