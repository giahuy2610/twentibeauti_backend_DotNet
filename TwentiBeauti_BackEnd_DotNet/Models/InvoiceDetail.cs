using Org.BouncyCastle.Asn1.X509;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class InvoiceDetail
    {
        
        public int IDInvoice { get; set; }
        
        public int IDProduct { get; set; }
        public int Quantity { get; set; }
    }
}
