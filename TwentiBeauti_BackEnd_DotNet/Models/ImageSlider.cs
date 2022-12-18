using System.ComponentModel.DataAnnotations;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class ImageSlider
    {
        [Key]
        public int? IDImage { get; set; }
        public DateTime StartOn { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime EndOn { get; set; }
        public bool IsDeleted { get; set; }
        public string Path { get; set; }
        public string Route { get; set; }
    }
}
