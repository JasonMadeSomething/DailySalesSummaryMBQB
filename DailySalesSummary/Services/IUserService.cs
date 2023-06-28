using DailySalesSummary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DailySalesSummary.Services
{
    public interface IUserService
    {
        
        Task<IEnumerable<User>> GetUsers();
        Task<User> GetUser(string id);

        Task<bool> CreateUser(User user, string password);

        Task<bool> UpdateUser(User user);

        Task<bool> DeleteUser(string id);

        Task<User> Authenticate(string username, string password);  
    }
}
