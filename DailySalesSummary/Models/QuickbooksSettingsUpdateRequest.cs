namespace DailySalesSummary.Models
{
    public class QuickbooksSettingsUpdateRequest
    {
        public string UserId { get; set; }

        public QuickbooksSettings QuickbooksSettings { get; set; }
    }
}
