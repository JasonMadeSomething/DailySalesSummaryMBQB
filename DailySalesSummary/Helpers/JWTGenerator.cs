using DailySalesSummary.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DailySalesSummary.Helpers
{
    public class JWTGenerator : IJWTGenerator
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<User> _userManager;
        public JWTGenerator(UserManager<User> userManager, IConfiguration configuration)
        {
            _configuration = configuration;
            _userManager = userManager;
        }
        public async Task<string> GenerateJwtToken(User user)
        {
            IList<string> userRoles =  await _userManager.GetRolesAsync(user);
            var tokenHandler = new JwtSecurityTokenHandler();
            IConfiguration jwtSettings = _configuration.GetSection("JwtSettings");
            byte[] key = Encoding.UTF8.GetBytes(jwtSettings.GetValue<string>("Key"));
            string issuer = jwtSettings.GetValue<string>("Issuer");
            string audience = jwtSettings.GetValue<string>("Audience");

            var claims = new List<Claim>
            {
                new Claim("id", user.Id.ToString()),
            };

            foreach (var userRole in userRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, userRole));
            }

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7), // Token expiration, change as per your needs
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = issuer,
                Audience = audience
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
