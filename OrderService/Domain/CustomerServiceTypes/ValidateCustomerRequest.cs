using System.Text.Json.Serialization;

namespace Domain.CustomerServiceTypes;

public class ValidateCustomerRequest
{
    [JsonPropertyName("customerId")]
    public Guid CustomerId { get; set; }

    [JsonPropertyName("shippingAddressId")]
    public Guid ShippingAddressId { get; set; }

    [JsonPropertyName("mobilePhone")]
    public string MobilePhone { get; set; }
}