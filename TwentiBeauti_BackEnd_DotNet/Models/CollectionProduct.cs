using Org.BouncyCastle.Asn1.X509;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class CollectionProduct
    {
        public int? IDCollection { get; set; }
        public int? IDProduct { get; set; }
    }
}
