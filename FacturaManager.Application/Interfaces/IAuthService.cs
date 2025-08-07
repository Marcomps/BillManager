using FacturaManager.Application.Contracts.Auth;

namespace FacturaManager.Application.Interfaces;
public interface IAuthService
{
    LoginResponse? Login(LoginRequest request);
}