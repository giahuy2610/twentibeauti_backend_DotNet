using System.ComponentModel.DataAnnotations;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class InvoiceDetail
    {
        [Key]
        public int IDInvoice { get; set; }
        public int IDProduct { get; set; }
        public int Quantity { get; set; }
    }
}
