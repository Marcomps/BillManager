namespace FacturaManager.Domain.Entities;
public class ExtraField : BaseEntity
{
    public string EntityType { get; set; } = string.Empty; // "Client", "Supplier", etc.
    public Guid EntityId { get; set; } // ID del cliente o proveedor
    public string FieldName { get; set; } = string.Empty;
    public string FieldValue { get; set; } = string.Empty;
}