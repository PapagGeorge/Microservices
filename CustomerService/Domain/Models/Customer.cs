using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Net;

namespace Domain.Models;

public class Customer
{
    [Key]
    [JsonPropertyName("id")]
    public Guid Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; }

    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("mobilePhone")]
    public string MobilePhone { get; set; }

    [JsonPropertyName("address")]
    public Address Address { get; set; }

    [JsonPropertyName("status")]
    public CustomerStatus Status { get; set; }

    [JsonPropertyName("loyaltyPoints")]
    public decimal LoyaltyPoints { get; set; }

    public IEnumerable<Address> Addresses { get; set; }
}

public enum CustomerStatus
{
    Active,
    Inactive
}