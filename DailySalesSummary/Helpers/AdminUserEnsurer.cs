using AspNetCore.Identity.Mongo.Model;
using DailySalesSummary.Models;
using Microsoft.AspNetCore.Identity;

namespace Mindbody.Helpers
{
    public class AdminUserEnsurer
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<MongoRole> _roleManager;
        private readonly IConfiguration _configuration;

        public AdminUserEnsurer(UserManager<User> userManager, RoleManager<MongoRole> roleManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task EnsureCreatedAsync()
        {
            MongoRole adminRole = new MongoRole { Name = "Admin" };
            MongoRole systemRole = new MongoRole { Name = "System" };
            if (await _roleManager.FindByNameAsync(adminRole.Name) == null)
            {
                await _roleManager.CreateAsync(adminRole);
            }

            if (await _roleManager.FindByNameAsync(systemRole.Name) == null)
            {
                await _roleManager.CreateAsync(systemRole);
            }

            if (!_userManager.Users.Any(u => u.UserName == _configuration.GetSection("Admin:AdminUsername").Value))
            {
                var adminUser = new User {
                    UserName = _configuration.GetSection("Admin:AdminUsername").Value,
                    SecurityStamp = Guid.NewGuid().ToString()
                };
                await _userManager.CreateAsync(adminUser, _configuration.GetSection("Admin:AdminPassword").Value);
                await _userManager.AddToRoleAsync(adminUser, adminRole.Name);
            }
        }
    }

}
