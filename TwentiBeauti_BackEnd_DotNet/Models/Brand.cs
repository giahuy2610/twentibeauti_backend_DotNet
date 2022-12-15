using System.ComponentModel.DataAnnotations;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class Brand
    {
        [Key]
        public int? IDBrand { get; set; }
        public string NameBrand { get; set; }
        public int? IDCollection { get; set; }
        public string? Country { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool? IsDeleted { get; set; }
        public int? TotalProduct { get; set; }
        public int? TotalPurchaseQuantity { get; set; }
    }
}
