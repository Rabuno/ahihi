using TetPee.Service.Base;

namespace TetPee.Service.User;

public interface IService
{
    public Task<Base.Response.PageResult<Response.GetUsersResponse>> GetUsers(
        string? searchTerm,
        int pageSize, 
        int pageIndex);
    public Task<string> CreateUser(Request.CreateUserRequest request);
    
    public Task<string> UpdateUser(Guid id, Request.UpdateUserRequest request);
    
    public Task<string> DeleteUser(Guid id);
}