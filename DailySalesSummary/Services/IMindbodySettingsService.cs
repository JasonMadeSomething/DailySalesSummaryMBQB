using DailySalesSummary.Models;
namespace DailySalesSummary.Services
{
    public interface IMindbodySettingsService
    {
        Task<MindbodySettings> GetMindbodySettings(string userId);

        Task<User> SetMindbodySettings(MindbodySettingsUpdateRequest mindbodySettingsUpdateRequest);
    }
}
