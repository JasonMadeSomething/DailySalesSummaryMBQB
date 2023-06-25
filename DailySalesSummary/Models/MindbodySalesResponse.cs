namespace DailySalesSummary.Models
{
    public class MindbodySalesResponse
    {
        public PaginationResponse PaginationResponse { get; set; }
        public List<Sale> Sales { get; set; }
    }
}
