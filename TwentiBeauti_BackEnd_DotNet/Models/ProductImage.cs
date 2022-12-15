using System.ComponentModel.DataAnnotations;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class ProductImage
    {
        [Key]
        public int? IDProductImage { get; set; }
        public int IDProduct { get; set; }
        public string Path { get; set; }
    }
}
