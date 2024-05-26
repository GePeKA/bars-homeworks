using TaskManager.Dtos;

namespace TaskManager.Abstractions.Services
{
    public interface IAuthService
    {
        Task AuthenticateAsync(SigninDto authDto);
        Task<long> RegisterAsync(SignupDto authDto);
        Task ConfirmEmailAsync(string confirmToken, string email);
        Task ChangeUserRoleOnAdmin(string userEmail);
    }
}
