using Task6.Data.Models;

namespace Task6.Data.Services
{
    public interface IUserService
    {
        Task<List<string>> GetAllUserNamesAsync();
        Task<User> GetUserByNameAsync(string userName);
        Task<User> CreateUserAsync(string name);
    }
}