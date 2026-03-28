namespace TetPee.Service.User;

public class Request
{
    public class CreateUserRequest
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }

    public class UpdateUserRequest : CreateUserRequest
    {
    }
}