using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Domain.Models
{
    [DataContract]
    public class ProductQuantity
    {
        [Key]
        [JsonPropertyName("id")]
        public Guid Id { get; set; }

        [JsonPropertyName("orderId")]
        public Guid OrderId { get; set; }

        [JsonPropertyName("productId")]
        public Guid ProductId { get; set; }

        [JsonPropertyName("unitPrice")]
        public decimal UnitPrice { get; set; }

        [JsonPropertyName("quantity")]
        public int Quantity { get; set; }

        public Order Order { get; set; }
    }
}
