using FacturaManager.Domain.Interfaces;

namespace FacturaManager.Domain.Entities;
public class User : BaseEntity, IAuditableEntity, ITenantEntity
{
    public string Username { get; set; } = string.Empty;
    public string PasswordHash { get; set; } = string.Empty;
    public Guid RoleId { get; set; }
    public Role Role { get; set; }

    public Guid TenantId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
}