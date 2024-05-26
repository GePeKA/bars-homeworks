namespace TaskManager.Abstractions.Services
{
    public interface IEmailService
    {
        Task SendEmailAsync(string email, string message);
    }
}
