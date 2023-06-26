using DailySalesSummary.Models;
using Microsoft.AspNetCore.Mvc;

namespace DailySalesSummary.Services
{
    public interface IUserService
    {
        

        Task<User> GetUser(string id);

        Task<User> CreateUser(User user);

        Task<User> UpdateUser(User user);

        Task<bool> DeleteUser(string id);

        Task<User> Authenticate(string username, string password);  
    }
}
