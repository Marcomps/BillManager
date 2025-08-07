namespace FacturaManager.Application.Contracts.Clients
{
    public class UpdateClientRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Address { get; set; }
        public string NIT { get; set; } = string.Empty;
        public string DUI { get; set; } = string.Empty;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string ModifiedBy { get; set; } = string.Empty;
    }
}