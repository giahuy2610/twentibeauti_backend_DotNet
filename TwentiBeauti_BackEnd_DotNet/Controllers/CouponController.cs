using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Org.BouncyCastle.Pkcs;
using TwentiBeauti_BackEnd_DotNet.Data;
using TwentiBeauti_BackEnd_DotNet.Models;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{

    [ApiController]
    [Route("api/coupon")]
    public class CouponController : ControllerBase
    {
        private readonly Context dbContextCoupon;
        public CouponController(Context dbContextCoupon)
        {
            this.dbContextCoupon = dbContextCoupon;
        }

        [HttpGet("index")]
        public async Task<IActionResult> index()
        {
            return (new JsonResult(await dbContextCoupon.Coupon.ToListAsync()));
        }

        [HttpGet("available")]
        public async Task<IActionResult> indexAvailable()
        {
            return (Ok(await dbContextCoupon.Coupon.Where(c => c.StartOn <= DateTime.Now && c.EndOn >= DateTime.Now && c.Stock > 0 && c.IsDeleted == false).ToListAsync()));
        }

        [HttpGet("show/{IDCoupon:int}")]
        public async Task<IActionResult> show(int IDCoupon)
        {
            return Ok(await dbContextCoupon.Coupon.FindAsync(IDCoupon));
        }

        [HttpGet("check-available/{IDCoupon:int}")]
        public async Task<IActionResult> checkAvailable(int IDCoupon)
        {
            var c = await dbContextCoupon.Coupon.FindAsync(IDCoupon);
            if (c == null) return NotFound();
            else if (c.StartOn <= DateTime.Now && c.EndOn >= DateTime.Now && c.Stock > 0 && c.IsDeleted == false)
            return Ok(c);
            else return BadRequest();
        }

        [HttpPost]
        [Route("post/{CodeCoupon}")]
        public async Task<IActionResult> GetCoupon([FromRoute] string CodeCoupon)
        {

            return Ok(this.dbContextCoupon.Coupon.Where(coupon => coupon.CodeCoupon == CodeCoupon).First().IDCoupon);
        }

    }

}
