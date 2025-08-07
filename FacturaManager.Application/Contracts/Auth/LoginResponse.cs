namespace FacturaManager.Application.Contracts.Auth;
public class LoginResponse
{
    public Guid UserId { get; set; }
    public string Username { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;
}