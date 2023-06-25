using MongoDB.Driver;
using MongoDB.Bson;
using DailySalesSummary.Models;
using System.Diagnostics;

namespace DailySalesSummary.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly IMongoCollection<User> _users;
        public UserRepository(IMongoClient client)
        {
            var database = client.GetDatabase("MBQBDev");
            _users = database.GetCollection<User>("users");
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _users.Find(user => true).ToListAsync();
        }

        public async Task<User> GetUser(string id)
        {
            return await _users.Find<User>(user => user.Id == id).FirstOrDefaultAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            await _users.InsertOneAsync(user);
            return user;
        }

        public async Task<User> UpdateUser(User userIn)
        {
            
            await _users.ReplaceOneAsync(user => user.Id == userIn.Id, userIn);
            return userIn;
        }

        public async Task<bool> DeleteUser(string id)
        {
            var result = await _users.DeleteOneAsync(user => user.Id == id);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }
    }
}
