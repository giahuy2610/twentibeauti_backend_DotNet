using Microsoft.AspNetCore.Mvc;
using TwentiBeauti_BackEnd_DotNet.Models;
using TwentiBeauti_BackEnd_DotNet.Services;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/")]
    public class VNPayController : Controller
    {
        private readonly IVnPayService _vnPayService;

        public VNPayController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }


        [HttpGet("test/")]
        public IActionResult CreatePaymentUrl([FromQuery]int TotalValue, [FromQuery] int IDInvoice)
        {
            PaymentInformationModel model = new PaymentInformationModel()
            {
                OrderType = "billpayment",
                Amount = TotalValue,
                OrderDescription = "Thanh toán đơn hàng",
                Name = "Khách hàng",
                Ref = IDInvoice.ToString(),
            };
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }
        [HttpGet("vnpay-return")]
        public IActionResult PaymentCallback()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            return Json(response);
        }

    }
}