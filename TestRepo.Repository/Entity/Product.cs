using TetPee.Repository.Abtraction;

namespace TetPee.Repository.Entity;

public class Product : BaseEntity<Guid>, IAuditableEntity
{
    public string Name { get; set; } = string.Empty;
    public float Price { get; set; }
    
    public User? User { get; set; }
    public Guid UserId { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}