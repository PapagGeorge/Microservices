using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Domain.Types
{
    [DataContract]
    public class ValidateCustomerRequest
    {
        [JsonPropertyName("customerId")]
        public Guid CustomerId { get; set; }

        [JsonPropertyName("mobilePhone")]
        public string MobilePhone { get; set; }

        [JsonPropertyName("shippingAddressId")]
        public Guid ShippingAddressId { get; set; }
    }
}
