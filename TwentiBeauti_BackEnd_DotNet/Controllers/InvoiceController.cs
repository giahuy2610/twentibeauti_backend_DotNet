using Microsoft.AspNetCore.Mvc;
using TwentiBeauti_BackEnd_DotNet.Data;
using TwentiBeauti_BackEnd_DotNet.Models;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    public class InvoiceController : ControllerBase
    {
        private readonly Context dbContextInvoice;
        public InvoiceController(Context dbContextInvoice)
        {
            this.dbContextInvoice = dbContextInvoice;
        }

        [HttpPost]
        [Route("invoice/create")]
        public async Task<IActionResult> CreateInvoice()
        {
            //var coupon = this.dbContextInvoice.CoupGetCoupon();
            var newInvoice = new Invoice();
            
            

                

            this.dbContextInvoice.Invoice.Add(newInvoice);
            return Ok();
        }

        public async Task<IActionResult> CreateInvocie1()
        {
            return Ok();
        }
    }
}
