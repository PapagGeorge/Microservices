using Domain.Types;

namespace Application.Services
{
    public static class ValidationExtensions
    {
        public static void Validate(this ValidateCustomerRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "Request cannot be null.");

            if (request.MobilePhone == string.Empty)
                throw new ArgumentException("Mobile Phone is required.", nameof(request.CustomerId));

            if (request.ShippingAddressId == Guid.Empty)
                throw new ArgumentException("ShippingAddressId is required.", nameof(request.ShippingAddressId));
        }
    }
}
