namespace TwentiBeauti_BackEnd_DotNet.Models
{
    public class PaymentInformationModel
    {
        public string OrderType { get; set; }
        public double Amount { get; set; }
        public string OrderDescription { get; set; }
        public string Name { get; set; }
        public string Ref { get; set; }
    }
}
