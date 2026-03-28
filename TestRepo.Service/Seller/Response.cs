namespace TetPee.Service.Seller;

public class Response
{
    public class GetSellersResponse
    {
        public Guid Id { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string TaxCode { get; set; }
        public string Email { get; set; }
    }
}