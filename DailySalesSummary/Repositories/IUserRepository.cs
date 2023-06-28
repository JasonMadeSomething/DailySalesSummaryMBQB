using DailySalesSummary.Models;
using Microsoft.AspNetCore.Identity;

namespace DailySalesSummary.Repositories
{
    public interface IUserRepository
    {
        
        Task<User> GetUser(string id);
        Task<IEnumerable<User>> GetAllUsers();
        // User Create
        Task<IdentityResult> CreateUser(User user, string password, string role);
        // User Update
        Task<bool> UpdateUser(User userIn);
        // User Delete
        Task<bool> DeleteUser(string id);

        Task<User> FindByNameAsync(string username);

        Task<bool> CheckPasswordAsync(User user, string password);
    }
}
