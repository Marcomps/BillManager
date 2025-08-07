namespace FacturaManager.Application.Contracts.Users;
public class UpdateUserRequest
{
    public Guid Id { get; set; } // Para saber qué usuario actualizar
    public string? Password { get; set; } // Se puede omitir si no se va a cambiar
    public Guid? RoleId { get; set; } // Se puede actualizar o dejar igual

    public string? ModifiedBy { get; set; }
}