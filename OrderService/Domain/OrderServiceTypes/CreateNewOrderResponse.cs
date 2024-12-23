using System.Text.Json.Serialization;
using Domain.Models;

public class CreateNewOrderResponse
{
    [JsonPropertyName("isSuccess")]
    public bool IsSuccess { get; set; }

    [JsonPropertyName("message")]
    public string Message { get; set; }

    [JsonPropertyName("orderId")]
    public Guid? OrderId { get; set; }

    [JsonPropertyName("status")]
    public OrderStatus? Status { get; set; }
}