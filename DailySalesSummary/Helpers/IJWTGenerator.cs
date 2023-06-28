using DailySalesSummary.Models;

namespace DailySalesSummary.Helpers
{
    public interface IJWTGenerator
    {
        Task<string> GenerateJwtToken(User user);
    }
}
