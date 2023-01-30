using MongoDB.Driver;
using Task6.Data.Models;

namespace Task6.Data.Services
{
    public class EmailService : IEmailService
    {
        private IMongoClient _client;
        private IMongoCollection<Email> _emails;

        public EmailService(IMongoDbSettings settings, IMongoClient client)
        {
            _client = client;
            var database = _client.GetDatabase(settings.DatabaseName);
            _emails = database.GetCollection<Email>("Emails");
        }
        public async Task SendEmailAsync(Email email)
        {
            await _emails.InsertOneAsync(email);
        }

        public async Task<IEnumerable<Email>> GetUserEmailsAsync(string user)
        {
            var filter = Builders<Email>.Filter.Eq(u => u.Recipient, user);
            var result = await _emails.Find(filter).ToListAsync();

            return result;
        }
    }
}
