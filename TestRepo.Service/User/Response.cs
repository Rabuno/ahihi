namespace TetPee.Service.User;

public class Response
{
    public class GetUsersResponse
    {
        public Guid Id { get; set; }
        public string Email { get; set; }
        public int Role { get; set; }
    }
}