using MongoDB.Driver;
using MongoDB.Bson;
using DailySalesSummary.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver.Linq;

namespace DailySalesSummary.Repositories
{
    public class UserRepository : IUserRepository
    {
    
        private readonly UserManager<User> _userManager;

        public UserRepository(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        

        public async Task<User> GetUser(string id)
        {
            
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<User> CreateUser(User user)
        {
            IdentityResult result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
                return user;
            else
                throw new Exception("User creation failed");
            
        }

        public async Task<User> UpdateUser(User userIn)
        {
            
            await _userManager.UpdateAsync(userIn);
            return userIn;
        }

        public async Task<bool> DeleteUser(string id)
        {
            var result = await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));

            return result.Succeeded;
        }

        public async Task<User> FindByNameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            var result = await _userManager.CheckPasswordAsync(user, password);
            return user;
        }
    }
}
