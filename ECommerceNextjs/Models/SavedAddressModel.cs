using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceNextjs.Models
{
    public class SavedAddressModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? _id {  get; set; }
        public string email { get; set; } = null!;
        public string fullName { get; set; } = null!;
        public string addressLineOne { get; set; } = null!;
        public string city { get; set; } = null!;
        public string state { get; set; } = null!;
        public string zipCode { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;

        public Boolean setAsDefault { get; set; }
    }

    public class SetAsDefault
    {
        public string id { get; set; } = null!;
        public bool value { get; set; }
        public string email { get; set; } = null!;

        public string property { get; set; } = null!;
    }
}
