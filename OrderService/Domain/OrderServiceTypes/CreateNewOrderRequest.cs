using System.Text.Json.Serialization;

public class CreateNewOrderRequest
{
    [JsonPropertyName("customerId")]
    public Guid CustomerId { get; set; }

    [JsonPropertyName("items")]
    public List<OrderItemRequest> Items { get; set; } = new List<OrderItemRequest>();

    [JsonPropertyName("shippingAddressId")]
    public Guid ShippingAddressId { get; set; }

    [JsonPropertyName("mobilePhone")]
    public string MobilePhone { get; set; }
}