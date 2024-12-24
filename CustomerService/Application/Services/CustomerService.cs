using Application.Interfaces;
using Domain.Types;

namespace Application.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }
        public async Task<ValidateCustomerResponse> ValidateCustomerAsync(ValidateCustomerRequest request)
        {
            request.Validate();

            var customer = await _customerRepository.GetCustomerByIdAsync(request.CustomerId);
            if (customer == null)
            {
                var response = new ValidateCustomerResponse();
                response.IsValid = false;
                response.ValidationMessage = "Customer not found.";
                return response;
            }

            if (!string.IsNullOrWhiteSpace(request.MobilePhone) && customer.MobilePhone != request.MobilePhone)
            {
                var response = new ValidateCustomerResponse();
                response.IsValid = false;
                response.ValidationMessage = "Mobile phone number does not match.";
                return response;
            }

            var addresses = await _customerRepository.GetAddressesByCustomerIdAsync(request.CustomerId);
            if (!addresses.Any(a => a.Id == request.ShippingAddressId))
            {
                var response = new ValidateCustomerResponse();
                response.IsValid = false;
                response.ValidationMessage = "Shipping address is invalid or does not belong to the customer.";
                return response;
            }

            var validResponse = new ValidateCustomerResponse();
            validResponse.IsValid = true;
            validResponse.ValidationMessage = "Customer is valid.";
            return validResponse;
        }
    }
}
