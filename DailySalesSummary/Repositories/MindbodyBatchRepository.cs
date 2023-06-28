using System.Collections;
using DailySalesSummary.Models;
using MongoDB.Driver;

namespace DailySalesSummary.Repositories
{
    public class MindbodyBatchRepository : IMindbodyBatchRepository
    {
        private readonly IMongoCollection<MindbodyBatchReport> _mindbodyBatchReports;
        public MindbodyBatchRepository(IMongoClient client)
        {
            var database = client.GetDatabase("MBQBDev");
            _mindbodyBatchReports = database.GetCollection<MindbodyBatchReport>("mbBatchReports");
        }
        public async Task<IEnumerable<MindbodyBatchReport>> GetAllAsync()
        {
            return await _mindbodyBatchReports.Find(mindbodyBatchReport => true).ToListAsync();
        }

        public async Task<MindbodyBatchReport> GetByIdAsync(string batchId)
        {
            return await _mindbodyBatchReports.Find<MindbodyBatchReport>(mindbodyBatchReport => mindbodyBatchReport.batchId == batchId).FirstOrDefaultAsync();
        }

        public async Task<MindbodyBatchReport> CreateAsync(MindbodyBatchReport mindbodyBatchReport)
        {
            await _mindbodyBatchReports.InsertOneAsync(mindbodyBatchReport);
            return mindbodyBatchReport;
        }
        public async Task<MindbodyBatchReport> UpdateAsync(MindbodyBatchReport mindbodyBatchReportIn)
        {
            await _mindbodyBatchReports.ReplaceOneAsync(mindbodyBatchReport => mindbodyBatchReport.batchId == mindbodyBatchReportIn.batchId, mindbodyBatchReportIn);
            return mindbodyBatchReportIn;
        }

        public async Task<bool> DeleteAsync(string batchId)
        {
            var result = await _mindbodyBatchReports.DeleteOneAsync(mindbodyBatchReport => mindbodyBatchReport.batchId == batchId);
            return result.IsAcknowledged && result.DeletedCount > 0;
        }

        public async Task<IEnumerable<MindbodyBatchReport>> GetByUserAsync(User user)
        {
            return await _mindbodyBatchReports.Find(mindbodyBatchReport => mindbodyBatchReport.UserIds.Contains(user.Id.ToString())).ToListAsync();
        }

        public async Task<IEnumerable<MindbodyBatchReport>> GetByDateRangeAsync(DateTime startDate, DateTime endDate)
        {
            return await _mindbodyBatchReports.Find(mindbodyBatchReport => mindbodyBatchReport.StartDate >= startDate && mindbodyBatchReport.EndDate <= endDate).ToListAsync();
        }

        public async Task<IEnumerable<MindbodyBatchReport>> GetByDateRangeAndUserAsync(DateTime startDate, DateTime endDate, User user)
        {
            return await _mindbodyBatchReports.Find(mindbodyBatchReport => mindbodyBatchReport.StartDate >= startDate && mindbodyBatchReport.EndDate <= endDate && mindbodyBatchReport.UserIds.Contains(user.Id.ToString())).ToListAsync();
        }

    }
}
