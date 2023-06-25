using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DailySalesSummary.Models
{
    public class MindbodySalesDataBatch
    {
        public MindbodySalesDataBatch()
        {
            Id = Guid.NewGuid().ToString();
            Sales = new List<Sale>();
        }
        [BsonId]
        public string Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string UserId { get; set; }

        public List<Sale> Sales { get; set; }

        public DateTime FetchedAt { get; set; }
    }
}
