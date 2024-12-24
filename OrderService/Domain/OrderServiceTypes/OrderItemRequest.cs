using System.Text.Json.Serialization;

public class OrderItemRequest
{
    [JsonPropertyName("productId")]
    public Guid ProductId { get; set; }

    [JsonPropertyName("unitPrice")]
    public decimal UnitPrice { get; set; }

    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}