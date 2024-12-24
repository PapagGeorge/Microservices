using System.Net.Http.Json;
using Application.Interfaces;
using Domain.CustomerServiceTypes;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Repos;

public class CustomerHttpClientRepository : ICustomerHttpClientRepository
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public CustomerHttpClientRepository(IHttpClientFactory httpClientFactory, IConfiguration configuration)
    {
        _httpClient = httpClientFactory.CreateClient("CustomerService");
        _configuration = configuration;
    }
    
    public async Task<ValidateCustomerResponse> ValidateCustomerAsync(ValidateCustomerRequest request)
    {
        var BaseUrl = _configuration.GetSection("ApiSettings:CustomerServiceBaseUrl").Value;

        var requestUri = $"{BaseUrl}api/customers/validateCustomer" +
            $"?customerId={request.CustomerId}" +
            $"&mobilePhone={request.MobilePhone}" +
            $"&shippingAddressId={request.ShippingAddressId}";

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