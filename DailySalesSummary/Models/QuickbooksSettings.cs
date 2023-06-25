namespace DailySalesSummary.Models
{
    public class QuickbooksSettings
    {
        public string CompanyId { get; set; }
        public string AuthToken { get; set; } // Encrypted or hashed
    }
}
