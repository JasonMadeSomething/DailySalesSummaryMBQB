using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using Microsoft.AspNetCore.Identity;
using AspNetCore.Identity.Mongo.Model;

namespace DailySalesSummary.Models
{
    public class User : MongoUser
    {
        public MindbodySettings? Mindbody { get; set; }
        public QuickbooksSettings? Quickbooks { get; set; }
    }
}
