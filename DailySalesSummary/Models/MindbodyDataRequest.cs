namespace DailySalesSummary.Models
{
    public class MindbodyDataRequest
    {
        public string UserId { get; set; }
        
        public MindbodySettings? MindbodySettings { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
