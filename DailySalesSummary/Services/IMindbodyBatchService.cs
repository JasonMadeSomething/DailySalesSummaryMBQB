using DailySalesSummary.Models;

namespace DailySalesSummary.Services
{
    public interface IMindbodyBatchService
    {

        Task<MindbodyBatchReport> RunBatchForAllUsers(string triggeringUser);
    }
}
