using System.ComponentModel.DataAnnotations;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class Product
    {
        [Key]
        public int? IDProduct { get; set; }
        public string NameProduct { get; set; }
        public int? IDBrand { get; set; }
        public string? Description { get; set; }
        public DateTime? CreatedOn { get; set; }
        public bool IsDeleted { get; set; }
        public int Stock { get; set; }
        public int TotalPurchaseQuantity { get; set; }
        public int Mass { get; set; }
        public string UnitsOfMass { get; set; }
        public string Units { get; set; }
        public int ApplyTaxes { get; set; }
        public int IDTag { get; set; }
        public int IDType { get; set; }
        public int ListPrice { get; set; }
        public bool StatusSale { get; set; }

        public static void getProductDetailByID(int IDProduct)
        {
            //get product
            //get the product current retail price
            //get the product brand
            //get the product images
            //get the product avg rating
            //get the product reviews
            }
        }
    
}
