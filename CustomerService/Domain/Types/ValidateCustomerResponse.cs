using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace Domain.Types
{
    [DataContract]
    public class ValidateCustomerResponse
    {
        [JsonPropertyName("isValid")]
        public bool IsValid { get; set; }

        [JsonPropertyName("validationMessage")]
        public string ValidationMessage { get; set; }
    }
}
