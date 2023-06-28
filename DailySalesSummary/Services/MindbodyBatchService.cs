using DailySalesSummary.Models;

namespace DailySalesSummary.Services
{
    public class MindbodyBatchService : IMindbodyBatchService
    {

        private readonly IMindbodyClientService _mindbodyClientService;

        public MindbodyBatchService(IMindbodyClientService mindbodyClientService)
        {
            _mindbodyClientService = mindbodyClientService;
        }

        public async Task<MindbodyBatchReport> RunBatchForAllUsers(string triggeringUser)
        {
            MindbodyBatchReport report = await _mindbodyClientService.RunBatchForAllUsers(triggeringUser);
            return report;
        }

    }
}
