using MongoDB.Driver;
using MongoDB.Bson;
using DailySalesSummary.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Identity;
using MongoDB.Driver.Linq;
using AspNetCore.Identity.Mongo.Model;

namespace DailySalesSummary.Repositories
{
    public class UserRepository : IUserRepository
    {
    
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<MongoRole> _roleManager;
        public UserRepository(UserManager<User> userManager, RoleManager<MongoRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userManager.GetUsersInRoleAsync("User");
        }

        public async Task<User> GetUser(string id)
        {
            
            return await _userManager.FindByIdAsync(id);
        }

        public async Task<IdentityResult> CreateUser(User user, string password, string roleName)
        {
            IdentityResult result = await _userManager.CreateAsync(user, password);
            string errors = "";
            if (!result.Succeeded)
            {
                errors = string.Join(", ", result.Errors.Select(e => e.Description));
                throw new Exception($"User creation failed: {errors}");
            }
            if (!await _roleManager.RoleExistsAsync(roleName))
            {
                var role = new MongoRole { Name = roleName };
                var roleResult = await _roleManager.CreateAsync(role);
                if (!roleResult.Succeeded)
                {
                    errors = string.Join(", ", roleResult.Errors.Select(e => e.Description));
                    throw new Exception($"Role creation failed: {errors}");
                }
            }
            IdentityResult roleAssignmentResult = await _userManager.AddToRoleAsync(user, roleName);

            if (!roleAssignmentResult.Succeeded)
            {
                errors = string.Join(", ", roleAssignmentResult.Errors.Select(e => e.Description));
                throw new Exception($"Role assignment failed: {errors}");
            }

            return result;

        }

        public async Task<bool> UpdateUser(User userIn)
        {

            IdentityResult result = await _userManager.UpdateAsync(userIn);
            return result.Succeeded;
        }

        public async Task<bool> DeleteUser(string id)
        {
            IdentityResult result = await _userManager.DeleteAsync(await _userManager.FindByIdAsync(id));

            return result.Succeeded;
        }

        public async Task<User> FindByNameAsync(string username)
        {
            return await _userManager.FindByNameAsync(username);
        }

        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            bool result = await _userManager.CheckPasswordAsync(user, password);
            return result;
        }
    }
}
