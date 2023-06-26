using DailySalesSummary.Models;

namespace DailySalesSummary.Helpers
{
    public interface IJWTGenerator
    {
        string GenerateJwtToken(User user);
    }
}
