using DailySalesSummary.Models;
using System.Runtime.CompilerServices;

namespace DailySalesSummary.Repositories
{
    public interface IUserRepository
    {
        
        Task<User> GetUser(string id);
        
        // User Create
        Task<User> CreateUser(User user);
        // User Update
        Task<User> UpdateUser(User userIn);
        // User Delete
        Task<bool> DeleteUser(string id);

        Task<User> FindByNameAsync(string username);

        Task<bool> CheckPasswordAsync(User user, string password);
    }
}
