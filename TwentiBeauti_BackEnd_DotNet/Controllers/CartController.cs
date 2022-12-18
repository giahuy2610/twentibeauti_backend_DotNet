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
    [Route("api/cart")]

    public class CartController : ControllerBase
    {
        private readonly Context dbContextCart;
        public CartController(Context dbContextCart)
        {
            this.dbContextCart = dbContextCart;
        }


        [HttpPost("show")]
        public async Task<IActionResult> ShowCart(dynamic request)
        {
            return Ok(JsonConvert.SerializeObject(await Show(request)));
        }

        [ApiExplorerSettings(IgnoreApi = true)]
        public async Task<List<dynamic>> Show(dynamic request)
        {
            var IDCus = 0;
            IDCus = request["IDCus"];
            var cartitems = dbContextCart.Cart.Where(c => c.IDCus == IDCus).ToList();
            List<dynamic> data = new List<dynamic>();
            foreach(var cartitem in cartitems)
            {
                
                var json = JsonConvert.SerializeObject(await new ProductController(dbContextCart).show(cartitem.IDProduct));
                dynamic cartItemDetail = JsonConvert.DeserializeObject(json, typeof(ExpandoObject));
                cartItemDetail.Quantity = cartitem.Quantity;
                data.Add(cartItemDetail);
            }

            return (data);
        }

        [HttpPost("update")]
        public async Task<IActionResult> UpdateCart(dynamic request)
        {
            var IDCus = 0;
            IDCus = request["IDCus"];
            var IDProduct = 0;
            IDProduct = request["IDProduct"];
            var cartitems = dbContextCart.Cart.Where(c => c.IDCus == IDCus && c.IDProduct == IDProduct).FirstOrDefault();
            if (cartitems == null) {

                if (request["IsAdd"] == 1 || request["IsAdd"] == true)
                {
                    //create
                    var cartitem = new Cart();
                    cartitem.IDCus = IDCus;
                    cartitem.IDProduct = IDProduct;
                    cartitem.Quantity = 1;
                    dbContextCart.Cart.Add(cartitem);
                    dbContextCart.SaveChanges();
                    
                }
                else return NotFound(); 
            } 
            else
            {
                if (request["IsAdd"] == 1 || request["IsAdd"] == true)
                {
                    cartitems.Quantity += 1;
                }
                else
                {
                    if (cartitems.Quantity - 1 > 0)
                    {
                        cartitems.Quantity -= 1;
                    }
                    else
                    {
                        dbContextCart.Remove(cartitems);
                    }
                }
                dbContextCart.SaveChanges();
                
            }
            return Ok(JsonConvert.SerializeObject(await Show(request)));
        }

        [HttpPost("destroy")]
        public async Task<IActionResult> Destroy(dynamic request)
        {
            var IDCus = 0;
            IDCus = request["IDCus"];
            var IDProduct = 0;
            IDProduct = request["IDProduct"];
            var cartItem = dbContextCart.Cart.Where(c => c.IDCus == IDCus && c.IDProduct == IDProduct).FirstOrDefault();
            dbContextCart.Remove(cartItem);
            dbContextCart.SaveChanges();
            return Ok(JsonConvert.SerializeObject(await Show(request)));
        }
    }
}