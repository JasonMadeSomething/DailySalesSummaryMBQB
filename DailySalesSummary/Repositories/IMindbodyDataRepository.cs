using DailySalesSummary.Models;
using System.Runtime.CompilerServices;

namespace DailySalesSummary.Repositories
{
    public interface IMindbodyDataRepository
    {
        Task<IEnumerable<Sale>> GetAllSalesData();

        Task<Sale> UpdateSalesData(Sale sale);

        Task<Sale> CreateSalesData(Sale sale);

        Task<bool> CheckIfSaleExists(Sale sale);

        Task<IEnumerable<Sale>> GetSalesByBatchId(string batchId);

        Task<IEnumerable<Sale>> GetSalesByUser(User user);

        Task<IEnumerable<Sale>> BulkAddSales(MindbodySalesDataBatch salesDataBatch);
    }
}
