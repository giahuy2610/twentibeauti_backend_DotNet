using Microsoft.AspNetCore.Mvc;
using TwentiBeauti_BackEnd_DotNet.Models;
using TwentiBeauti_BackEnd_DotNet.Services;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/")]
    public class VNPayController : Controller
    {
        public readonly IVnPayService _vnPayService;

        public VNPayController(IVnPayService vnPayService)
        {
            _vnPayService = vnPayService;
        }


        [HttpGet("vnpay-send")]
        public IActionResult CreatePaymentUrl()
        {
            PaymentInformationModel model = new PaymentInformationModel() { 
                OrderType = "billpayment",
                Amount = 1000000,
                OrderDescription = "Thanh toán đơn số 1",
                Name = "Huy"
            };
            var url = _vnPayService.CreatePaymentUrl(model, HttpContext);

            return Redirect(url);
        }


    }
}
