namespace ECommerceNextjs.Models
{
    public class CheckoutCartModel
    {
        public List<CheckoutProductModel> products { get; set; } = null!;
        
       // public float subtotal { get; set; }
       // public float taxes  { get; set; }
        //public float totalPrice  { get; set; }


    }

    public class CheckoutProductModel
    {
        public string email { get; set; } = null!;
        public long id { get; set; }
        public string title { get; set; } = null!;
        public float price { get; set; }
        public string category { get; set; } = null!;
        public string description { get; set; } = null!;
        public string image { get; set; } = null!;
        public long quantity { get; set; }
    }
}
