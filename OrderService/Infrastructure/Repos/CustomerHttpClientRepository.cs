using System.Net.Http.Json;
using Application.Interfaces;
using Domain.CustomerServiceTypes;

namespace Infrastructure.Repos;

public class CustomerHttpClientRepository : ICustomerHttpClientRepository
{
    private readonly HttpClient _httpClient;

    public CustomerHttpClientRepository(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient("CustomerService");
    }
    
    public async Task<ValidateCustomerResponse> ValidateCustomerAsync(ValidateCustomerRequest request)
    {
        var requestUri = $"api/customers/validate?customerId={request.CustomerId}&shippingAddressId={request.ShippingAddressId}";

        try
        {
            var response = await _httpClient.GetAsync(requestUri);

            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<ValidateCustomerResponse>();
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                var errorMessage = $"Failed to validate customer. StatusCode: {response.StatusCode}, Content: {errorContent}";
                // When having logger (replace Console.WriteLine with actual logging)
                Console.WriteLine(errorMessage);

                // Return a meaningful default response indicating failure
                return new ValidateCustomerResponse
                {
                    IsValid = false,
                    ValidationMessage = "Customer validation failed due to an error."
                };
            }
        }
        catch (Exception ex)
        {
            throw new ApplicationException("An error occurred while validating the customer.", ex);
        }
    }
}