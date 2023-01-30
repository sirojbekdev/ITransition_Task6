using Task6.Data.Models;

namespace Task6.Data.Services
{
    public interface IEmailService
    {
        Task<IEnumerable<Email>> GetUserEmailsAsync(string user);
        Task SendEmailAsync(Email email);
    }
}