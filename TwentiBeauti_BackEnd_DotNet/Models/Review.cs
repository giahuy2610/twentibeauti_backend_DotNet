using System.ComponentModel.DataAnnotations;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class Review
    {
        [Key]
        public int? IDReview { get; set; }
        public int IDProduct { get; set; }
        public int IDCus { get; set; }
        public DateTime? CreatedOn { get; set; }
        public string ContentShort { get; set; }
        public string? ContentLong { get; set; }
        public int Rating { get; set; }
        public bool? IsDeleted { get; set; }
    
    
    
    }
}
