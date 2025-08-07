using FacturaManager.Application.Contracts.Clients;
using FacturaManager.Application.Interfaces;
using FacturaManager.Domain.Entities;
using FacturaManager.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FacturaManager.Application.Services;

public class ClientService : IClientService
{
    private readonly AppDbContext _context;

    public ClientService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<ClientDto>> GetAllClientsAsync(Guid tenantId)
    {
        return await _context.Clients
            .Where(c => c.TenantId == tenantId && !c.IsDeleted)
            .Select(c => new ClientDto
            {
                Id = c.Id,
                Name = c.Name,
                Address = c.Address,
                DUI = c.DUI,
                NIT = c.NIT,
                Email = c.Email,
                Phone = c.Phone,
                CreatedAt = c.CreatedAt,
                CreatedBy = c.CreatedBy,
                ModifiedAt = c.ModifiedAt,
                ModifiedBy = c.ModifiedBy,
                TenantId = c.TenantId
            })
            .ToListAsync();
    }


    public async Task<ClientDto?> GetClientByIdAsync(Guid id)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Id == id && !c.IsDeleted);

        if (client == null)
            return null;

        return new ClientDto
        {
            Id = client.Id,
            Name = client.Name,
            Address = client.Address,
            DUI = client.DUI,
            NIT = client.NIT,
            Email = client.Email,
            Phone = client.Phone,
            CreatedAt = client.CreatedAt,
            CreatedBy = client.CreatedBy,
            ModifiedAt = client.ModifiedAt,
            ModifiedBy = client.ModifiedBy,
            TenantId = client.TenantId
        };
    }


    public async Task<Guid> CreateClientAsync(CreateClientRequest request)
    {
        var exists = await _context.Clients
            .AnyAsync(c => (c.DUI == request.DUI || c.NIT == request.NIT) && !c.IsDeleted);

        if (exists)
            throw new InvalidOperationException("A client with the same DUI or NIT already exists.");

        var client = new Client
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Address = request.Address,
            DUI = request.DUI,
            NIT = request.NIT,
            Email = request.Email,
            Phone = request.Phone,
            TenantId = request.TenantId,
            CreatedAt = DateTime.UtcNow,
            CreatedBy = request.CreatedBy
        };

        _context.Clients.Add(client);
        await _context.SaveChangesAsync();

        return client.Id;
    }


    public async Task<bool> UpdateClientAsync(UpdateClientRequest request)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Id == request.Id && !c.IsDeleted);

        if (client == null)
            return false;

        var duplicate = await _context.Clients
            .AnyAsync(c =>
                c.Id != request.Id &&
                !c.IsDeleted &&
                (c.DUI == request.DUI || c.NIT == request.NIT));

        if (duplicate)
            throw new InvalidOperationException("Another client with the same DUI or NIT already exists.");

        client.Name = request.Name;
        client.Address = request.Address;
        client.DUI = request.DUI;
        client.NIT = request.NIT;
        client.Email = request.Email;
        client.Phone = request.Phone;
        client.ModifiedAt = DateTime.UtcNow;
        client.ModifiedBy = request.ModifiedBy;

        _context.Clients.Update(client);
        await _context.SaveChangesAsync();

        return true;
    }



    public async Task<bool> DeleteClientAsync(Guid clientId, string deletedBy)
    {
        var client = await _context.Clients
            .FirstOrDefaultAsync(c => c.Id == clientId && !c.IsDeleted);

        if (client == null)
            return false;

        client.IsDeleted = true;
        client.ModifiedAt = DateTime.UtcNow;
        client.ModifiedBy = deletedBy;

        _context.Clients.Update(client);
        await _context.SaveChangesAsync();

        return true;
    }

}
