using System.ComponentModel.DataAnnotations;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class Event
    {
        [Key]
        public int? IDEvent { get; set; }
        public string NameEvent { get; set; }
        public int ValueDiscount { get; set; }
        public int UnitsDiscount    { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime StartOn { get; set; }
        public DateTime EndOn { get; set; }
    }
}
