using TetPee.Repository.Abtraction;

namespace TetPee.Repository.Entity;

public class Category : BaseEntity<Guid>, IAuditableEntity
{
    public string? Name { get; set; }
    
    public Category? ParentCategory { get; set; }
    public Guid? ParentCategoryId { get; set; }
    
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}