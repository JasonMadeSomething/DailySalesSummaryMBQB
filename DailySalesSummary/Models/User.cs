using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace DailySalesSummary.Models
{
    public class User
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        public string Username { get; set; }
        public string Password { get; set; } 
        public MindbodySettings? Mindbody { get; set; }
        public QuickbooksSettings? Quickbooks { get; set; }
    }
}
