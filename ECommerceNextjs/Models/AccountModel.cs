using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceNextjs.Models
{
    public class AccountModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? _id { get; set; }
        public string fullName { get; set; } = null!;
        public string emailAddress { get; set; } = null!;
        public string accountCreated { get; set; } = null!;
        public string lastLoggedIn { get; set; } = null!;
    }
}
