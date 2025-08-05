using FacturaManager.Domain.Entities;
using FacturaManager.Domain.Interfaces;

namespace FacturaManager.Domain.Entities;

public class Supplier : BaseEntity, IAuditableEntity, ITenantEntity
{
    public string Name { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string NIT { get; set; } = string.Empty;
    public string DUI { get; set; } = string.Empty;

    public Guid TenantId { get; set; }

    public DateTime CreatedAt { get; set; }
    public DateTime? ModifiedAt { get; set; }
    public string? ModifiedBy { get; set; }
}
