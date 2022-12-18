using System.ComponentModel.DataAnnotations;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class Tag
    {
        [Key]
        public int IDTag { get; set; }
        public string NameTag { get; set; }
        public int IDType { get; set; }
    }
}
