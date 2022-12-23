using System.ComponentModel.DataAnnotations;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class PromotionRegister
    {
        [Key]
        public int IDRegister { get; set; }
        public string Email { get; set; }
        public int? IDCus { get; set; }
        public Boolean IsDeleted { get; set; }
    }
}
