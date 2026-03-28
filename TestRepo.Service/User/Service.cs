using Microsoft.EntityFrameworkCore;
using TetPee.Repository;

namespace TetPee.Service.User;

public class Service : IService
{
    private readonly AppDbContext _DbContext;

    public Service(AppDbContext dbContext)
    {
        _DbContext = dbContext;
    }
    
    public Task<Base.Response.PageResult<Response.GetUsersResponse>> GetUsers(string? searchTerm,
        int pageSize, 
        int pageIndex)
    {
        var query = _DbContext.Users.Where(x => true);
        
        if(searchTerm != null)
        {
            query = query.Where(x => 
                x.Email.Contains(searchTerm));
        }

        query = query.OrderBy(x => x.Email);
        
        query = query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
        
        var selectedQuery = query
            .Select(x => new Response.GetUsersResponse()
        {
            Id = x.Id,
            Email = x.Email,
            Role = x.Role,
        });

        var listResult = selectedQuery.ToList();
        var totalItems = listResult.Count();
        
        var result = new Base.Response.PageResult<Response.GetUsersResponse>()
        {
            Items = listResult,
            TotalItems = totalItems
        };
        
        return Task.FromResult(result);
    }
    public async Task<string> CreateUser(Request.CreateUserRequest request)
    {
        var existingUserQuery = _DbContext.Users.Where(u => u.Email == request.Email);
        
        bool isExistUser = await existingUserQuery.AnyAsync();

        if (isExistUser)
        {
            return "User with the same email already exists";
        }
        
        var user = new Repository.Entity.User()
        {
            Email = request.Email,
            Password = request.Password
        };
        
        _DbContext.Add(user);
        
        var  result = await _DbContext.SaveChangesAsync();
        
        return result >  0 ? "Add User Successfully" : "Add Product Failed";
    }
    
    public async Task<string> UpdateUser(Guid id, Request.UpdateUserRequest request)
    {
        var existingUserQuery = _DbContext.Users.Where(u => u.Id == id);
        
        var user = await existingUserQuery.FirstOrDefaultAsync();

        if (user == null)
        {
            return "User not found";
        }
        
        user.Email = request.Email;
        user.Password = request.Password;
        
        var result = await _DbContext.SaveChangesAsync();
        
        return result >  0 ? "Update User Successfully" : "Update User Failed";
    }
    
    public async Task<string> DeleteUser(Guid id)
    {
        var user = await _DbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        
        if (user == null)
            return "User not found";
        
        _DbContext.Users.Remove(user);    
        
        var result = await _DbContext.SaveChangesAsync();
        
        return result > 0 ? "Delete User Successfully" : "Delete User Failed";
    }
}