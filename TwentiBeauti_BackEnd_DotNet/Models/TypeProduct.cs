using System.ComponentModel.DataAnnotations;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class TypeProduct
    {
        [Key]
        public int IDType { get; set; }
        public string NameTypeProduct { get; set; }
    }
}
