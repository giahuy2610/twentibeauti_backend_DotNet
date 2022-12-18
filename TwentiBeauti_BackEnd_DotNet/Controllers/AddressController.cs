using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwentiBeauti_BackEnd_DotNet.Data;
using TwentiBeauti_BackEnd_DotNet.Models;
namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/address")]

    public class AddressController : ControllerBase
    {
        private readonly Context dbContextAddress;
        public AddressController(Context dbContextAddress)
        {
            this.dbContextAddress = dbContextAddress;
        }

        //public CustomerContext DbContext { get; set; }

        [HttpGet("get")]
        public async Task<IActionResult> GetAddress()
        {
            return (new JsonResult(await dbContextAddress.Address.ToListAsync()));
        }

        [HttpGet]
        [Route("get/{IDAddress:int}")]
        public async Task<IActionResult> GetAddress([FromRoute] int IDAddress)
        {
            var cus = await dbContextAddress.Address.FindAsync(IDAddress);
            if (cus == null)
            {
                return NotFound();
            }
            return Ok(cus);
        }
        [HttpPost("create")]
        public async Task<IActionResult> AddAddress(Address addAddressRequest)
        {
            var address = new Address()
            {
                //IDAddress = addAddressRequest.IDAddress,
                City = addAddressRequest.City,
                LastName = addAddressRequest.LastName,
                FirstName = addAddressRequest.FirstName,
                District = addAddressRequest.District,
                AddressDetail = addAddressRequest.AddressDetail,
                Ward = addAddressRequest.Ward,
                Email = addAddressRequest.Email,
                Phone = addAddressRequest.Phone,
                IsDeleted = addAddressRequest.IsDeleted,
            };
            await dbContextAddress.Address.AddAsync(address);
            await dbContextAddress.SaveChangesAsync();
            return Ok(address);
        }
    }
}
