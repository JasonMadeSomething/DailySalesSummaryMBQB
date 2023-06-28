using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace DailySalesSummary.Models
{
    public class Sale
    {
        public Sale()
        {
            dbId = System.Guid.NewGuid().ToString();
        }
        [BsonId]
        public string dbId { get; set; }
        public long? Id { get; set; }
        public DateTime? SaleDate { get; set; }
        public string SaleTime { get; set; }
        public DateTime? SaleDateTime { get; set; }
        public DateTime? OriginalSaleDateTime { get; set; }
        public long? SalesRepId { get; set; }
        public string ClientId { get; set; }
        public long? RecipientClientId { get; set; }
        public List<PurchasedItem> PurchasedItems { get; set; }
        public int? LocationId { get; set; }
        public List<SalePayment> Payments { get; set; }
        public string batchId { get; set; }
        public string userId { get; set; }
    }
}
