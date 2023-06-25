using DailySalesSummary.Models;
using DailySalesSummary.Repositories;

namespace DailySalesSummary.Services
{
    public class MindbodyDataService : IMindbodyDataService
    {
        private readonly IMindbodyDataRepository _mindbodyDataRepository;
        public MindbodyDataService(IMindbodyDataRepository mindbodyDataRepository ) 
        {
            _mindbodyDataRepository = mindbodyDataRepository;
        }

        public async Task<IEnumerable<Sale>> BulkAddSales(MindbodySalesDataBatch mindbodySalesDataBatch)
        {
            
            
            IEnumerable<Sale> addedSales = await _mindbodyDataRepository.BulkAddSales(mindbodySalesDataBatch);
                
            
            return addedSales;
        }

        public async Task<bool> CheckIfSaleExists(Sale sale)
        {
            bool saleExists = await _mindbodyDataRepository.CheckIfSaleExists(sale);
            return saleExists;
        }

        public async Task<Sale> CreateSalesData(Sale sale)
        {
            Sale createdSale = await _mindbodyDataRepository.CreateSalesData(sale);
            return createdSale;
        }

        public async Task<IEnumerable<Sale>> GetAllSalesData()
        {
            IEnumerable<Sale> allSales = await _mindbodyDataRepository.GetAllSalesData();
            return allSales;
        }

        public async Task<IEnumerable<Sale>> GetSalesByBatchId(string batchId)
        {
            IEnumerable<Sale> salesByBatchId = await _mindbodyDataRepository.GetSalesByBatchId(batchId);
            return salesByBatchId;
        }

        public async Task<IEnumerable<Sale>> GetSalesByUser(User user)
        {
            IEnumerable<Sale> salesByUser = await _mindbodyDataRepository.GetSalesByUser(user);
            return salesByUser;
        }

        public async Task<Sale> UpdateSalesData(Sale sale)
        {
            Sale updatedSale = await _mindbodyDataRepository.UpdateSalesData(sale);
            return updatedSale;
        }

    }
}
