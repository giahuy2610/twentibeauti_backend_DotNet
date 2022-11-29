using System.ComponentModel.DataAnnotations;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class Address
    {
        [Key]
        public int IDAddress { get; set; }
        
        public string? City { get; set; }
        public string? District { get; set; }
        public string? AddressDetail { get; set; }
        public string? Ward { get; set; }
        public int? Phone { get; set; }
        public bool IsDeleted { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }

        
    }
}
