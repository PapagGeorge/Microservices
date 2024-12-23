using System.Text.Json.Serialization;

public class ValidateCustomerResponse
{
    [JsonPropertyName("isValid")]
    public bool IsValid { get; set; }

    [JsonPropertyName("validationMessage")]
    public string? ValidationMessage { get; set; }
}