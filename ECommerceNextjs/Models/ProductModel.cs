namespace ECommerceNextjs.Models
{
    public class ProductModel
    {
        public string email { get; set; } = null!;
        public long id { get; set; }
        public string title { get; set; } = null!;
        public float price { get; set; }
        public string category { get; set; } = null!;
        public string description { get; set; } = null!;
        public string image { get; set; } = null!;
        public Rating? rating { get; set; }
    }

    public class Rating
    {
        public float count { get; set; }
        public float rate { get; set; }
    }
}
