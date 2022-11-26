using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwentiBeauti_BackEnd_DotNet.Data;
using TwentiBeauti_BackEnd_DotNet.Models;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class CartController : ControllerBase
    {
        private readonly Context dbContextCart;
        public CartController(Context dbContextCart)
        {
            this.dbContextCart = dbContextCart;
        }

        //public CustomerContext DbContext { get; set; }

        [HttpGet("get")]
        public async Task<IActionResult> GetCart()
        {
            return Ok(await dbContextCart.Cart.ToListAsync());

        }
        public IActionResult getproduct()
        {
            return Ok();
        }
    }
}