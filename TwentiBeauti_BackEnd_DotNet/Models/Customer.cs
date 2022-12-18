using System.ComponentModel.DataAnnotations;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class Customer
    {
        [Key]
        public int? IDCus { get; set; }
        public string? FirstName { get; set; }

        public string? LastName { get; set; }
        public int? Phone { get; set; }
        public string? Email { get; set; }
        public bool? IsDeleted { get; set; }
        public string? UID { get; set; }
    }
}
