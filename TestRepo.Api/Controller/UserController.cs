using Microsoft.AspNetCore.Mvc;
using TetPee.Service.User;

namespace TestRepo.Api.Controller;

[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    public readonly IService _service;
    
    public UserController(IService service)
    {
        _service = service;
    }

    [HttpGet("")]
    public async Task<IActionResult> GetUsers(string? searchTerm, int pageSize = 10, int pageIndex = 1)
    {
        var users = await _service.GetUsers(searchTerm, pageSize, pageIndex);
        return Ok(users);
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateUser(Request.CreateUserRequest request)
    {
        var result = await _service.CreateUser(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(Guid id ,Request.UpdateUserRequest request)
    {
        var result = await _service.UpdateUser(id, request);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        var result = await _service.DeleteUser(id);
        return Ok(result);
    }
}