namespace TetPee.Service.Seller;

public class Request
{
    public class CreateSellerRequest : User.Request.CreateUserRequest
    {
        public Guid UserId { get; set; }
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string TaxCode { get; set; }
    }

    public class UpdateSellerRequest
    {
        public string CompanyName { get; set; }
        public string CompanyAddress { get; set; }
        public string TaxCode { get; set; }
    }
}