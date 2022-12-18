using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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

        [HttpGet("index")] //done
        public async Task<IActionResult> index()
        {
            return Ok(JsonConvert.SerializeObject(await dbContextCoupon.Coupon.ToListAsync()));
        }

        [HttpGet("available")] // done
        public async Task<IActionResult> indexAvailable()
        {
            return Ok(JsonConvert.SerializeObject(await dbContextCoupon.Coupon.Where(c => c.StartOn <= DateTime.Now && c.EndOn >= DateTime.Now && c.Stock > 0 && c.IsDeleted == false).ToListAsync()));
        }

        [HttpGet("show/{IDCoupon:int}")] // done
        public async Task<IActionResult> show(int IDCoupon)
        {
            return Ok(JsonConvert.SerializeObject(await dbContextCoupon.Coupon.FindAsync(IDCoupon)));
        }

        [HttpGet("check-available/{CodeCoupon}")] // done
        public async Task<IActionResult> checkAvailable(String CodeCoupon)
        {
            var c = dbContextCoupon.Coupon.Where(coupon => coupon.CodeCoupon == CodeCoupon).First();
            if (c == null) return NotFound();
            else if (c.StartOn <= DateTime.Now && c.EndOn >= DateTime.Now && c.Stock > 0 && c.IsDeleted == false)
            return Ok(JsonConvert.SerializeObject(c));
            else return BadRequest();
        }
    }

}
