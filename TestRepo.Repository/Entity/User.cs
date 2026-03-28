using TetPee.Repository.Abtraction;

namespace TetPee.Repository.Entity;

public class User : BaseEntity<Guid>, IAuditableEntity
{
    public string Password { get; set; }
    public string Email { get; set; }
    public int Role { get; set; }
    
    public Seller? Seller { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}