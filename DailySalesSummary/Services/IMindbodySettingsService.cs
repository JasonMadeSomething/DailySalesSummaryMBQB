using DailySalesSummary.Models;
namespace DailySalesSummary.Services
{
    public interface IMindbodySettingsService
    {
        Task<MindbodySettings> GetMindbodySettings(string userId);

        Task<bool> SetMindbodySettings(MindbodySettingsUpdateRequest mindbodySettingsUpdateRequest);
    }
}
