using MongoDB.Bson.Serialization.Attributes;

namespace ECommerceNextjs.Models
{
    public class OrderSummaryModel
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? _id { get; set; }
        public string dateOrdered { get; set; } = null!;
        public string orderNumber { get; set; } = null!;
        public string orderStatus { get; set; } = null!;
        public List<OrderProduct> products { get; set; } = null!;
        //public List<ProductModel> products { get; set; } = null!;
        public string email { get; set; } = null!;
        public ShippingInfo shippingInfo { get; set; } = null!;
    }

    public class OrderProduct
    {
        public ProductModel product { get; set; } = null!;
        public long quantity { get; set; }
    }

    public class ShippingInfo
    {
        public string email { get; set; } = null!;
        public string name { get; set; } = null!;
        public string address { get; set; } = null!;
        public string city { get; set; } = null!;
        public string state { get; set; } = null!;
        public string zipCode { get; set; } = null!;
        public string phoneNumber { get; set; } = null!;
        public string deliveryType { get; set; } = null!;
    }
}
