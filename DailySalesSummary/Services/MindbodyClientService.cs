using DailySalesSummary.Models;
using DailySalesSummary.Repositories;

namespace DailySalesSummary.Services
{
    public class MindbodyClientService : IMindbodyClientService
    {
        private readonly IMindbodyClient _mindbodyClient;
        private readonly IMindbodyDataService _mindbodyDataService;
        private readonly IMindbodyBatchReportService _mindbodyBatchReportService;
        private readonly IMindbodySettingsService _mindbodySettingsService;
        private readonly IUserRepository _userRepository;
        public MindbodyClientService(IMindbodyClient mindbodyClient, IMindbodyDataService mindbodyDataService, IMindbodyBatchReportService mindbodyBatchReportService, IMindbodySettingsService mindbodySettingsService, IUserRepository userRepository)
        {
            _mindbodyClient = mindbodyClient;
            _mindbodyDataService = mindbodyDataService;
            _mindbodyBatchReportService = mindbodyBatchReportService;
            _mindbodySettingsService = mindbodySettingsService;
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<Sale>> GetMindbodySalesDataAsync(MindbodyDataRequest mindbodyDataRequest, string userId)
        {
            DateTime startTimestamp = DateTime.Now;

            if (mindbodyDataRequest.StartDate == null || mindbodyDataRequest.EndDate == null)
            {
                mindbodyDataRequest.StartDate = DateTime.Now.AddDays(-1);
            } 
            User user = await _userRepository.GetUser(userId);
            MindbodySalesDataBatch mindbodySalesData = await _mindbodyClient.GetMindbodySalesDataAsync(mindbodyDataRequest, user);
            
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
                UserIds = new List<string> { userId },
                TriggeringUser = userId
            };

            await _mindbodyBatchReportService.CreateBatchReportAsync(mindbodyBatchReport);
            return addedSales;
        }

        public async Task<MindbodyBatchReport> RunBatchForAllUsers(string triggeringUserId)
        {
            DateTime startTimestamp = DateTime.Now;


            List<User> users = _userRepository.GetAllUsers().Result.ToList();

            MindbodySalesDataBatch mindbodySalesData = new MindbodySalesDataBatch();
            MindbodyDataRequest request = new MindbodyDataRequest();
            request.StartDate = DateTime.Now.AddDays(-1);
            request.EndDate = DateTime.Now;
            foreach (User user in users) {
                MindbodySalesDataBatch sales = await _mindbodyClient.GetMindbodySalesDataAsync(request, user);
                
                mindbodySalesData.Sales.AddRange(sales.Sales);
            
            }

            if (mindbodySalesData == null || mindbodySalesData == new MindbodySalesDataBatch())
            {
                return null;
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
                UserIds = users.Select(u => u.Id.ToString()).ToList(),
                TriggeringUser = triggeringUserId
            };

            await _mindbodyBatchReportService.CreateBatchReportAsync(mindbodyBatchReport);

            return mindbodyBatchReport;

        }
    }
}
