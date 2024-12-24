using Domain.Types;

namespace Application.Interfaces
{
    public interface ICustomerService
    {
        Task<ValidateCustomerResponse>ValidateCustomerAsync(ValidateCustomerRequest request);
    }
}
