namespace ECommerceNextjs.Models
{
    public class PaymentMethodModel
    {
            public string cardNumber { get; set; } = null!;
            public string expiryDate { get; set; } = null!;
            public string cvv { get; set; } = null!;
            public string fullName { get; set; } = null!;
            public string billingZipCode { get; set; } = null!;
    }
}
