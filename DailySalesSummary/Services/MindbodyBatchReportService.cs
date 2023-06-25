using DailySalesSummary.Models;
using DailySalesSummary.Repositories;

namespace DailySalesSummary.Services
{
    public class MindbodyBatchReportService : IMindbodyBatchReportService
    {
        private readonly IMindbodyBatchRepository _mindbodyBatchRepository;

        public MindbodyBatchReportService(IMindbodyBatchRepository mindbodyBatchRepository)
        {
            _mindbodyBatchRepository = mindbodyBatchRepository;
        }

        public async Task<IEnumerable<MindbodyBatchReport>> GetAllAsync()
        {
            return await _mindbodyBatchRepository.GetAllAsync();
        }

        public async Task<MindbodyBatchReport> GetByIdAsync(string id)
        {
            return await _mindbodyBatchRepository.GetByIdAsync(id);
        }

        public async Task<MindbodyBatchReport> CreateBatchReportAsync(MindbodyBatchReport mindbodyBatchReport)
        {
            return await _mindbodyBatchRepository.CreateAsync(mindbodyBatchReport);
        }

        public async Task<MindbodyBatchReport> UpdateBatchReportAsync(MindbodyBatchReport mindbodyBatchReport)
        {
            return await _mindbodyBatchRepository.UpdateAsync(mindbodyBatchReport);
        }

        public async Task<bool> DeleteAsync(string id)
        {
            return await _mindbodyBatchRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<MindbodyBatchReport>> GetByUserAsync(User user)
        {
            return await _mindbodyBatchRepository.GetByUserAsync(user);
        }
        public async Task<IEnumerable<MindbodyBatchReport>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _mindbodyBatchRepository.GetByDateRangeAsync(startDate, endDate);
        }

        public async Task<IEnumerable<MindbodyBatchReport>> GetByDateRangeAndUserAsync(DateTime startDate, DateTime endDate, User user)
        {
            return await _mindbodyBatchRepository.GetByDateRangeAndUserAsync(startDate, endDate, user);
        }



    }
}
