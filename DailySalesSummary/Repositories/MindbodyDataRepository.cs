using MongoDB.Driver;
using DailySalesSummary.Models;

namespace DailySalesSummary.Repositories
{
    public class MindbodyDataRepository : IMindbodyDataRepository
    {
        private readonly IMongoCollection<Sale> _mindbodySalesData;

        public MindbodyDataRepository(IMongoClient client)
        {
            var database = client.GetDatabase("MBQBDev");
            _mindbodySalesData = database.GetCollection<Sale>("mbSalesData");
        }

        public async Task<IEnumerable<Sale>> GetAllSalesData()
        {
            return await _mindbodySalesData.Find(mindbodySalesData => true).ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetSalesByBatchId(string batchId)
        {
            return await _mindbodySalesData.Find<Sale>(mindbodySalesData => mindbodySalesData.batchId == batchId).ToListAsync();
        }

        public async Task<IEnumerable<Sale>> GetSalesByUser(User user)
        {
            return await _mindbodySalesData.Find<Sale>(mindbodySalesData => mindbodySalesData.userId == user.Id).ToListAsync();
        }

        public async Task<Sale> UpdateSalesData(Sale mindbodySalesDataIn)
        {
            await _mindbodySalesData.ReplaceOneAsync(mindbodySalesData => (mindbodySalesData.Id == mindbodySalesDataIn.Id && mindbodySalesDataIn.userId == mindbodySalesData.userId), mindbodySalesDataIn);
            return mindbodySalesDataIn;
        }

        public async Task<Sale> CreateSalesData(Sale sale)
        {
            await _mindbodySalesData.InsertOneAsync(sale);
            return sale;
        }

        public async Task<bool> CheckIfSaleExists(Sale sale)
        {
            var result = await _mindbodySalesData.Find(mindbodySalesData => (mindbodySalesData.Id == sale.Id && mindbodySalesData.userId == sale.userId)).FirstOrDefaultAsync();
            return result != null;
        }

        public async Task<IEnumerable<Sale>> BulkAddSales(MindbodySalesDataBatch salesDataBatch)
        {
            List<Sale> salesToAdd = new List<Sale>();
            foreach(Sale sale in salesDataBatch.Sales)
            {
                if (!(await CheckIfSaleExists(sale)))
                {
                    salesToAdd.Add(sale);
                }
            }
            if (salesToAdd.Count == 0)
            {
                return salesToAdd;
            }
            await _mindbodySalesData.InsertManyAsync(salesToAdd);
            return salesToAdd;
        }
    }
}
