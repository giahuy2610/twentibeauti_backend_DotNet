using System.ComponentModel.DataAnnotations;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class Cart
    {
        public int IDCus { get; set; }
        public int IDProduct { get; set; }
        public int? Quantity { get; set; }
    }
}


