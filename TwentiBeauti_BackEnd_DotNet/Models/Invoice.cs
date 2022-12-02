using System.ComponentModel.DataAnnotations;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class Invoice
    {
        [Key]
        public int IDInvoice { get; set; }
        public int IDTracking { get; set; }
        public int IDAddress { get; set; }

        public int? IDCoupon { get; set; }

        public int IDCus { get; set; }
        public string? Note { get; set; }
        public int TotalValue { get; set; }
        public int MethodPay { get; set; }

        public int MethodTransfer { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsPrintInvoice { get; set; }

        public bool IsPaid { get; set; }
    }
}
