namespace DailySalesSummary.Models
{
    public class SalePayment
    {
        public long? Id { get; set; }
        public decimal? Amount { get; set; }
        public int? Method { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
        public string TransactionId { get; set; }
    }
}
