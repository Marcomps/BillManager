using FacturaManager.Application.Contracts.Users;
using FacturaManager.Application.Interfaces;
using FacturaManager.Domain.Entities;
using FacturaManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace FacturaManager.Application.Services;

public class UserService : IUserService
{
    private readonly AppDbContext _dbContext;

    public UserService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public void Create(CreateUserRequest request)
    {
        // Validar que no exista el username
        if (_dbContext.Users.Any(u => u.Username == request.Username && !u.IsDeleted))
            throw new Exception("Username already exists");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = request.Username,
            PasswordHash = HashPassword(request.Password),
            RoleId = request.RoleId,
            TenantId = Guid.NewGuid(), // Puedes controlar esto desde la UI
            CreatedAt = DateTime.UtcNow,
            CreatedBy = request.CreatedBy
        };

        _dbContext.Users.Add(user);
        _dbContext.SaveChanges();
    }

    public void Update(UpdateUserRequest request)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == request.Id && !u.IsDeleted);
        if (user is null)
            throw new Exception("User not found");

        if (!string.IsNullOrWhiteSpace(request.Password))
            user.PasswordHash = HashPassword(request.Password);

        if (request.RoleId.HasValue)
            user.RoleId = request.RoleId.Value;

        user.ModifiedAt = DateTime.UtcNow;
        user.ModifiedBy = request.ModifiedBy;

        _dbContext.SaveChanges();
    }

    public void Delete(Guid userId)
    {
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId && !u.IsDeleted);
        if (user is null)
            throw new Exception("User not found");

        user.IsDeleted = true;
        user.ModifiedAt = DateTime.UtcNow;
        _dbContext.SaveChanges();
    }

    public UserDto? GetById(Guid userId)
    {
        var user = _dbContext.Users
            .Include(u => u.Role)
            .FirstOrDefault(u => u.Id == userId && !u.IsDeleted);

        return user is null ? null : new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Role = user.Role?.Name ?? "Unknown",
            TenantId = user.TenantId,
            CreatedAt = user.CreatedAt,
            ModifiedAt = user.ModifiedAt,
            ModifiedBy = user.ModifiedBy
        };
    }

    private static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        var bytes = Encoding.UTF8.GetBytes(password);
        var hash = sha256.ComputeHash(bytes);
        return Convert.ToBase64String(hash);
    }
}
