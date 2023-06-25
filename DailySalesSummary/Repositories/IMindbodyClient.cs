using DailySalesSummary.Models;

namespace DailySalesSummary.Repositories
{
    public interface IMindbodyClient
    {
        Task<MindbodySalesDataBatch> GetMindbodySalesDataAsync(MindbodyDataRequest mindbodyDataRequest);
    }
}
