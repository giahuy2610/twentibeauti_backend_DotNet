using System.ComponentModel.DataAnnotations;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class Coupon
    {
        [Key]
        public int? IDCoupon { get; set; }
        public int ValueDiscount { get; set; }
        public DateTime StartOn { get; set; }

        public DateTime EndOn { get; set; }
        public string? Description { get; set; }
        public bool IsDeleted { get; set; }
        public int MinInvoiceValue { get; set; }
        public string CodeCoupon { get; set; }

        public int Quantity { get; set; }
        public bool? IsMutualEvent { get; set; }

        public int Stock { get; set; }
    }
}
