using Domain.CustomerServiceTypes;

namespace Application.Interfaces;

public interface ICustomerHttpClientRepository
{
    Task<ValidateCustomerResponse> ValidateCustomerAsync(ValidateCustomerRequest request);
}