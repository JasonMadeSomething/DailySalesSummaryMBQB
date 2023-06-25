using DailySalesSummary.Models;
using DailySalesSummary.Repositories;

namespace DailySalesSummary.Services
{
    public class MindbodyClientService : IMindbodyClientService
    {
        private readonly IMindbodyClient _mindbodyClient;
        private readonly IMindbodyDataService _mindbodyDataService;
        private readonly IMindbodyBatchReportService _mindbodyBatchReportService;
        public MindbodyClientService(IMindbodyClient mindbodyClient, IMindbodyDataService mindbodyDataService, IMindbodyBatchReportService mindbodyBatchReportService)
        {
            _mindbodyClient = mindbodyClient;
            _mindbodyDataService = mindbodyDataService;
            _mindbodyBatchReportService = mindbodyBatchReportService;
        }

        public async Task<IEnumerable<Sale>> GetMindbodySalesDataAsync(MindbodyDataRequest mindbodyDataRequest)
        {
            DateTime startTimestamp = DateTime.Now;

            if (mindbodyDataRequest.StartDate == null || mindbodyDataRequest.EndDate == null)
            {
                mindbodyDataRequest.StartDate = DateTime.Now.AddDays(-1);
            } 
            MindbodySalesDataBatch mindbodySalesData = await _mindbodyClient.GetMindbodySalesDataAsync(mindbodyDataRequest);
            
            if (mindbodySalesData == null || mindbodySalesData == new MindbodySalesDataBatch())
            {
                return new List<Sale>();
            }
            
            int totalRecords = mindbodySalesData.Sales.Count();

            IEnumerable<Sale> addedSales = await _mindbodyDataService.BulkAddSales(mindbodySalesData);

            DateTime endTimestamp = DateTime.Now;
            int updatedRecords = addedSales.Count();
            MindbodyBatchReport mindbodyBatchReport = new MindbodyBatchReport()
            {
                batchId = mindbodySalesData.Id,
                StartDate = startTimestamp,
                EndDate = endTimestamp,
                totalRecords = totalRecords,
                updatedRecords = updatedRecords,
                UserId = mindbodyDataRequest.UserId
            };

            await _mindbodyBatchReportService.CreateBatchReportAsync(mindbodyBatchReport);
            return addedSales;
        }

    }
}
