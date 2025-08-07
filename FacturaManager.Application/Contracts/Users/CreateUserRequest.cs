namespace FacturaManager.Application.Contracts.Users;
public class CreateUserRequest
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public Guid RoleId { get; set; }

    // Campos opcionales del sistema
    public string? CreatedBy { get; set; }
}