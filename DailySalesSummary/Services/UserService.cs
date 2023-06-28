using DailySalesSummary.Repositories;
using DailySalesSummary.Models;
using Microsoft.AspNetCore.Identity;
using AspNetCore.Identity.MongoDbCore.Models;

namespace DailySalesSummary.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        
        public UserService(IUserRepository userRepository) {
            _userRepository = userRepository;
            
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            return await _userRepository.GetAllUsers();
        }
        public async Task<User> GetUser(string id)
        {
            return await _userRepository.GetUser(id);
        }

        public async Task<bool> CreateUser(User user, string password)
        {
             IdentityResult result = await _userRepository.CreateUser(user, password, "User");
             return result.Succeeded;
        }

        public async Task<bool> CreateAdmin(User user, string password)
        {
            IdentityResult result = await _userRepository.CreateUser(user, password, "Admin");
            return result.Succeeded;
        }


        public async Task<bool> UpdateUser(User userIn)
        {
            return await _userRepository.UpdateUser(userIn);
        }

        public async Task<bool> DeleteUser(string id)
        {
            return await _userRepository.DeleteUser(id);
        }

        public async Task<User> Authenticate(string username, string password)
        {
            User user = await _userRepository.FindByNameAsync(username);
            if (user == null)
            {
                // User not found
                return null;
            }

            var passwordCheck = await _userRepository.CheckPasswordAsync(user, password);
            if (!passwordCheck)
            {
                // Password does not match
                return null;
            }

            // User found and password is correct. 
            // You might want to return only a DTO with the necessary data instead of the entire User object
            return user;
        }

    }
}
