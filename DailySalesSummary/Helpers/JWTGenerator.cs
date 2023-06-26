using DailySalesSummary.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DailySalesSummary.Helpers
{
    public class JWTGenerator : IJWTGenerator
    {
        private readonly IConfiguration _configuration;

        public JWTGenerator(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JwtSettings:Key"));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("id", user.Id.ToString()),
                    // You can add more claims if needed
                }),
                Expires = DateTime.UtcNow.AddDays(7), // Token expiration, change as per your needs
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
