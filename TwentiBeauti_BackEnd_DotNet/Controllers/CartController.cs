using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TwentiBeauti_BackEnd_DotNet.Data;
using TwentiBeauti_BackEnd_DotNet.Models;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/cart/")]

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
            var list = from cart in await dbContextCart.Cart.ToListAsync() select cart;
            String jsonString = JsonSerializer.Serialize(list);
            return Ok(jsonString);
        }




            //public IActionResult getproduct()
            //{
            //    return Ok();
            //}
        }
}