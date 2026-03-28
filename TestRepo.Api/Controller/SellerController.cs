using Microsoft.AspNetCore.Mvc;
using TetPee.Service.Seller;

namespace TestRepo.Api.Controller;

[ApiController]
[Route("[controller]")]
public class SellerController : ControllerBase
{
    private readonly IService _service;
    
    public SellerController(IService service)
    {
        _service = service;
    }
    
    [HttpGet("")]
    public async Task<IActionResult> GetSellers(string? searchTerm, int pageSize = 10, int pageIndex = 1)
    {
        var result = await _service.GetSellers(searchTerm, pageSize, pageIndex);
        return Ok(result);
    }

    [HttpPost("")]
    public async Task<IActionResult> CreateSeller(Request.CreateSellerRequest request)
    {
        var result = await _service.CreateSeller(request);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSeller(Guid id, Request.UpdateSellerRequest request)
    {
        var result = await _service.UpdateSeller(id, request);
        return Ok(result);
    }
}