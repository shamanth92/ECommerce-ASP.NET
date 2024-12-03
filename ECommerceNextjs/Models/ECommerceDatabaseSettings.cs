namespace ECommerceNextjs.Models
{
    public class ECommerceDatabaseSettings
    {
        public string ConnectionString { get; set; } = null!;
        public string DatabaseName { get; set; } = null!;
        public string CollectionName { get; set; } = null!;
        public string AddressCollectionName { get; set; } = null!;
        public string PaymentMethodCollectionName { get; set; } = null!;
        public string CheckoutCartCollection { get; set; } = null!;
        public string AccountCollectionName { get; set; } = null!;
        public string OrderSummaryCollection { get; set; } = null!;
    }
}
