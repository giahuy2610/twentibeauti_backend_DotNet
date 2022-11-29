using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TwentiBeauti_BackEnd_DotNet.Data;
using TwentiBeauti_BackEnd_DotNet.Models;

namespace TwentiBeauti_BackEnd_DotNet.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InvoiceDetailController : ControllerBase
    {
        private readonly Context dbContextInvoiceDetail;
        public InvoiceDetailController(Context dbContextInvoiceDetail)
        {
            this.dbContextInvoiceDetail = dbContextInvoiceDetail;
        }
        [HttpPut]
        [Route("update/{IDInvoice:int}")]
        public async Task<IActionResult> UpdateQuantityInvoice([FromRoute] int IDInvoice, InvoiceDetail updateInvoiceDetailRequest)
        {
            var invoiceDetail = await dbContextInvoiceDetail.InvoiceDetail.FindAsync(IDInvoice);

            if (invoiceDetail != null)
            {
                invoiceDetail.Quantity = updateInvoiceDetailRequest.Quantity;   

                await dbContextInvoiceDetail.SaveChangesAsync();
                return Ok(invoiceDetail);

            }
            return NotFound();


        }
    }
}
