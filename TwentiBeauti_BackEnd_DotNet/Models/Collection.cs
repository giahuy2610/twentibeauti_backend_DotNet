using System;
using System.ComponentModel.DataAnnotations;
using static System.Net.Mime.MediaTypeNames;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class Collection
    {
        [Key]
        public int? IDCollection { get; set; }
        public string NameCollection { get; set; }
        public string? RoutePath { get; set; }

        public DateTime? CreatedOn { get; set; }

        public string? Description { get; set; }

        public string? LogoImagePath { get; set; }

        public string? WallPaperPath { get; set; }

        public DateTime StartOn { get; set; }
        public DateTime EndOn { get; set; }
        public string? CoverImagePath { get; set; }

    }
}


