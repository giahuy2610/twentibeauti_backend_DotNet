namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class InvoiceInfo
    {
        public Invoice invoice { get; set; }
        public List<InvoiceDetail>? invoiceDetail { get; set; }

        public Address? address { get; set; }

        public InvoiceInfo(Invoice invoice, Address address = null, List< InvoiceDetail > invoiceDetail = null)
        {
            this.invoice = invoice;
            this.address = address;
            this.invoiceDetail = invoiceDetail;
        }
    }
}
