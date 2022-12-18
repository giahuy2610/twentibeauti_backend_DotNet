using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
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


        [HttpPost("create")]
        public async Task<IActionResult> create(dynamic request)
        {
            try
            {
                var coupon = new Coupon() {
                    ValueDiscount = request.ValueDiscount,
                    StartOn = request.StartOn,
                    EndOn = request.EndOn,
                    Description = request.Description,
                    MinInvoiceValue = request.MinInvoiceValue,
                    CodeCoupon = request.CodeCoupon,
                    Quantity = request.Quantity,
                    Stock = request.Quantity,
                    IsMutualEvent = request.IsMutualEvent
                };
                dbContextCoupon.Coupon.Add(coupon);
                dbContextCoupon.SaveChanges();
                return Ok(JsonConvert.SerializeObject(coupon));
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
                var coupon = dbContextCoupon.Coupon.Find((int)request["IDCoupon"]);
                coupon.ValueDiscount = request.ValueDiscount;
                coupon.StartOn = request.StartOn;
                coupon.EndOn = request.EndOn;
                coupon.Description = request.Description;
                coupon.MinInvoiceValue = request.MinInvoiceValue;
                coupon.CodeCoupon = request.CodeCoupon;
                coupon.Quantity = request.Quantity;
                coupon.Stock = request.Quantity;
                coupon.IsMutualEvent = request.IsMutualEvent;
                dbContextCoupon.SaveChanges();
                return Ok(JsonConvert.SerializeObject(coupon));
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }
    }

}
