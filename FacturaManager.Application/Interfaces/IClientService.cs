using FacturaManager.Application.Contracts.Clients;

namespace FacturaManager.Application.Interfaces
{
    public interface IClientService
    {
        Task<List<ClientDto>> GetAllClientsAsync(Guid tenantId);
        Task<ClientDto?> GetClientByIdAsync(Guid id);
        Task<Guid> CreateClientAsync(CreateClientRequest request);
        Task<bool> UpdateClientAsync(UpdateClientRequest request);
        Task<bool> DeleteClientAsync(Guid id, string deletedBy);
    }
}