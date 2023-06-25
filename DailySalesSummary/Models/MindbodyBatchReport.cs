using MongoDB.Bson.Serialization.Attributes;
namespace DailySalesSummary.Models
{
    public class MindbodyBatchReport
    {
        public string UserId { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [BsonId]
        public string batchId { get; set; }
        public int updatedRecords { get; set; }
        public int totalRecords { get; set; }

    }
}
