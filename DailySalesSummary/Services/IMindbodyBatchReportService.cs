using DailySalesSummary.Models;

namespace DailySalesSummary.Services
{
    public interface IMindbodyBatchReportService
    {

        Task<MindbodyBatchReport> CreateBatchReportAsync(MindbodyBatchReport mindbodyBatchReport);
        Task<MindbodyBatchReport> UpdateBatchReportAsync(MindbodyBatchReport mindbodyBatchReport);
        Task <IEnumerable<MindbodyBatchReport>> GetByUserAsync(User user);
        Task<IEnumerable<MindbodyBatchReport>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<MindbodyBatchReport>> GetByDateRangeAndUserAsync(DateTime startDate, DateTime endDate, User user);
        Task<IEnumerable<MindbodyBatchReport>> GetAllAsync();
        Task<MindbodyBatchReport> GetByIdAsync(string id);
        Task<bool> DeleteAsync(string id);

    }
}
