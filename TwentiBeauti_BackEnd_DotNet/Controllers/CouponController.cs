using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pkcs;
using TwentiBeauti_BackEnd_DotNet.Data;
using TwentiBeauti_BackEnd_DotNet.Models;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class CouponController : ControllerBase
    {
        private readonly Context dbContextCoupon;
        public CouponController(Context dbContextCoupon)
        {
            this.dbContextCoupon = dbContextCoupon;
        }

       
        [HttpPost]
        [Route("post/{CodeCoupon}")]
        public async Task<IActionResult> GetCoupon([FromRoute] string CodeCoupon)
        {

            return Ok(this.dbContextCoupon.Coupon.Where(coupon => coupon.CodeCoupon == CodeCoupon).First().IDCoupon);

            
            //return Context.GetCodeCoupon();
        }

    }

}
