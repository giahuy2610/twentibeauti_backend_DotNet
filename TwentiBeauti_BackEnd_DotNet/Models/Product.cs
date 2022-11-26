using System.ComponentModel.DataAnnotations;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class Product
    {
        [Key]
        public int? IDProduct { get; set; }
        public string NameProduct { get; set; }
        public int? IDBrand { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public int Stock { get; set; }
        public int TotalPurchaseQuantity { get; set; }
        public int Mass { get; set; }
        public string UnitsOfMass { get; set; }
        public string Units { get; set; }
        public int ApplyTaxes { get; set; }
        //public int
    }
}
