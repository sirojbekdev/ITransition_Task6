using MongoDB.Driver;
using Task6.Data.Models;

namespace Task6.Data.Services
{
    public class UserService : IUserService
    {
        private IMongoClient _client;
        private IMongoCollection<User> _users;

        public UserService(IMongoDbSettings settings, IMongoClient client)
        {
            _client = client;
            var database = _client.GetDatabase(settings.DatabaseName);
            _users = database.GetCollection<User>("Users");
        }

        public async Task<List<string>> GetAllUserNamesAsync()
        {
            return await _users.Find(_ => true).Project(x => x.Name).ToListAsync();
        }

        public async Task<User> GetUserByNameAsync(string userName)
        {
            var filter = Builders<User>.Filter.Eq(doc => doc.Name, userName);
            var result = await _users.FindAsync(filter);
            return result.FirstOrDefault();
        }

        public async Task<User> CreateUserAsync(string name)
        {
            User user = new()
            {
                Name = name,
            };
            await _users.InsertOneAsync(user);
            return user;
        }
    }
}
