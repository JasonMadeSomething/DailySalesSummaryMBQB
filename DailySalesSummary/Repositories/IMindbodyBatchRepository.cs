using DailySalesSummary.Models;

namespace DailySalesSummary.Repositories
{
    public interface IMindbodyBatchRepository
    {
        Task<IEnumerable<MindbodyBatchReport>> GetAllAsync();
        Task<MindbodyBatchReport> GetByIdAsync(string id);
        Task<MindbodyBatchReport> CreateAsync(MindbodyBatchReport mindbodyBatchReport);
        Task<MindbodyBatchReport> UpdateAsync(MindbodyBatchReport mindbodyBatchReport);
        Task<bool> DeleteAsync(string id);
        Task<IEnumerable<MindbodyBatchReport>> GetByUserAsync(User user);
        Task<IEnumerable<MindbodyBatchReport>> GetByDateRangeAsync(DateTime startDate, DateTime endDate);
        Task<IEnumerable<MindbodyBatchReport>> GetByDateRangeAndUserAsync(DateTime startDate, DateTime endDate, User user);
        

    }
}
