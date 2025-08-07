using FacturaManager.Application.Contracts.Users;

namespace FacturaManager.Application.Interfaces;

public interface IUserService
{
    UserDto? GetById(Guid userId);
    void Create(CreateUserRequest request);
    void Update(UpdateUserRequest request);
    void Delete(Guid userId); // Borrado lógico
}