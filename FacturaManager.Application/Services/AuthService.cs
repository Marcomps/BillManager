using FacturaManager.Application.Contracts.Auth;
using FacturaManager.Application.Interfaces;
using FacturaManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FacturaManager.Application.Services;

public class AuthService : IAuthService
{
    private readonly AppDbContext _dbContext;

    public AuthService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public LoginResponse? Login(LoginRequest request)
    {
        // Buscar usuario por nombre
        var user = _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefault(u => u.Username == request.Username && !u.IsDeleted);

        if (user is null)
            return null;

        // Verificar contraseña hasheada
        if (!VerifyPasswordHash(request.Password, user.PasswordHash))
            return null;

        return new LoginResponse
        {
            UserId = user.Id,
            Username = user.Username,
            Role = user.Role?.Name ?? "Unknown"
        };
    }

    private static bool VerifyPasswordHash(string password, string storedHash)
    {
        using var sha256 = SHA256.Create();
        var inputBytes = Encoding.UTF8.GetBytes(password);
        var inputHash = sha256.ComputeHash(inputBytes);
        var inputHashString = Convert.ToBase64String(inputHash);

        return storedHash == inputHashString;
    }
}