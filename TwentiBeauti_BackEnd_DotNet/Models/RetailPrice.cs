using System.ComponentModel.DataAnnotations;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class RetailPrice
    {
        [Key]
        public int? IDRetailPrice { get; set; }
        public int IDProduct { get; set; }
        public int Price { get; set; }
        public int IDEvent { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime StartOn { get; set; }
        public DateTime EndOn { get; set; }

    }
}
