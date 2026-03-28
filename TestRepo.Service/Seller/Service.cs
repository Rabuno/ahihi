using Microsoft.EntityFrameworkCore;
using TetPee.Repository;

namespace TetPee.Service.Seller;

public class Service : IService
{
    private readonly AppDbContext _dbContext;
    
    public Service(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<Base.Response.PageResult<Response.GetSellersResponse>> GetSellers(string? searchTerm, int pageSize, int pageIndex)
    {
        var query = _dbContext.Sellers.Where(x => true);
        
        if(searchTerm != null)
        {
            query = query.Where(x => 
                x.User.Email.Contains(searchTerm));
        }
        
        query = query.OrderBy(x => x.User.Email);
        
        query = query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize);
        
        var selectedQuery = query
            .Select(x => new Response.GetSellersResponse()
            {
                Id = x.User.Id,
                Email = x.User.Email,
                CompanyName = x.CompanyName,
                CompanyAddress = x.CompanyAddress,
                TaxCode = x.TaxCode
            });

        var listResult = await selectedQuery.ToListAsync();
        var totalItems = listResult.Count();
        
        var result = new Base.Response.PageResult<Response.GetSellersResponse>()
        {
            Items = listResult,
            TotalItems = totalItems
        };
        
        return result;
    }
    
    // public async Task<string> CreateSeller(Request.CreateSellerRequest request)
    // {
    //     var isExistSeller = _dbContext.Sellers.Any(s => s.UserId == request.UserId);
    //     
    //     if (isExistSeller)
    //         return "Seller already exists";
    //     
    //     var isExistUser = _dbContext.Users.Any(u => u.Id == request.UserId);
    //     
    //     if (!isExistUser)
    //         throw new  Exception("User not found");
    //     
    //     _dbContext.Sellers.Add(new Repository.Entity.Seller()
    //     {
    //         UserId = request.UserId,
    //         TaxCode = request.TaxCode,
    //         CompanyName = request.CompanyName,
    //         CompanyAddress = request.CompanyAddress
    //     });
    //     
    //     var result = await _dbContext.SaveChangesAsync();
    //     
    //     return result >  0 ? "Create Seller Successfully" : "Create Seller Failed";
    // }

    public async Task<string> CreateSeller (Request.CreateSellerRequest request)
    {
        var existingUserQuery = _dbContext.Users.Where(x => x.Email == request.Email);
        
        bool isExistUser = await existingUserQuery.AnyAsync();
        
        if(isExistUser)
        {
            throw new Exception("User Exist With Mail");
        }
        
        var user = new Repository.Entity.User()
        {
            Email = request.Email,
            Password = request.Password,
            Role = 2
        };

        _dbContext.Add(user);

        var result = await _dbContext.SaveChangesAsync();
        
        if (result > 0)
        {
            var seller = new Repository.Entity.Seller()
            {
                CompanyAddress = request.CompanyAddress,
                CompanyName = request.CompanyName,
                TaxCode = request.TaxCode,
                UserId = user.Id,
            };
            
            _dbContext.Add(seller);
            
            var sellerResult = await _dbContext.SaveChangesAsync();

            return sellerResult > 0 ? "Add Seller successfully" : "Add Seller failed";
        }

        return "Add Seller failed";
    }

    public async Task<string> UpdateSeller(Guid id, Request.UpdateSellerRequest request)
    {
        var isExistSeller = _dbContext.Sellers.Where(s => s.Id == id);
        
        var seller = await isExistSeller.FirstOrDefaultAsync();
        
        if (seller != null)
            return "Seller not exist";
        
        seller.CompanyAddress = request.CompanyAddress;
        seller.CompanyName = request.CompanyName;
        seller.TaxCode = request.TaxCode;
        
        var result = await _dbContext.SaveChangesAsync();
        
        return result > 0 ? "Update Seller Successfully" : "Update Seller Failed";
    }
}