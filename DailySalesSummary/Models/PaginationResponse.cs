namespace DailySalesSummary.Models
{
    public class PaginationResponse
    {
        public int RequestedLimit { get; set; }
        public int RequestedOffset { get; set; }
        public int PageSize { get; set; }
        public int TotalResults { get; set; }
    }
}
