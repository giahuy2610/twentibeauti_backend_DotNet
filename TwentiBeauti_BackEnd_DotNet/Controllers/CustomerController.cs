using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TwentiBeauti_BackEnd_DotNet.Data;
using TwentiBeauti_BackEnd_DotNet.Models;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CustomerController : ControllerBase
    {
        private readonly Context dbContext;
        public CustomerController(Context dbContext)
        {
            this.dbContext = dbContext;
        }

        //public CustomerContext DbContext { get; set; }

        [HttpGet("get")]
        public async Task<IActionResult> GetCustomers()
        {
            return new OkObjectResult(JsonSerializer.Serialize(await dbContext.Customer.ToListAsync()));

        }

        [HttpGet]
        [Route("get/{IDCus:int}")]
        public async Task<IActionResult> GetCustomer([FromRoute] int IDCus)
        {
            var cus = await dbContext.Customer.FindAsync(IDCus);
            String jsonString = JsonSerializer.Serialize(cus);
            if (cus == null)
            {
                return NotFound();
            }
            return new OkObjectResult(jsonString);
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
            return Ok(cus);
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
                return Ok(cus);

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
                return Ok(cus);

            }
            return NotFound();
        }

    }
}
