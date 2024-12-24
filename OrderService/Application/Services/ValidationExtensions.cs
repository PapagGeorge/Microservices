namespace Application.Services
{
    internal static class ValidationExtensions
    {
        internal static void Validate(this CreateNewOrderRequest request)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request), "Request cannot be null.");

            if (request.CustomerId == Guid.Empty)
                throw new ArgumentException("CustomerId is required.", nameof(request.CustomerId));

            if (request.ShippingAddressId == Guid.Empty)
                throw new ArgumentException("ShippingAddressId is required.", nameof(request.ShippingAddressId));

            if (string.IsNullOrWhiteSpace(request.MobilePhone))
                throw new ArgumentException("MobilePhone is required.", nameof(request.MobilePhone));

            if (request.Items == null || !request.Items.Any())
                throw new ArgumentException("Items collection cannot be null or empty.", nameof(request.Items));

            foreach (var item in request.Items)
            {
                if (item.ProductId == Guid.Empty)
                    throw new ArgumentException("Each item's ProductId is required.", nameof(item.ProductId));

                if (item.Quantity <= 0)
                    throw new ArgumentException("Each item's Quantity must be greater than zero.", nameof(item.Quantity));

                if (item.UnitPrice <= 0)
                    throw new ArgumentException("Each item's UnitPrice must be greater than zero.", nameof(item.UnitPrice));
            }
        }
    }
}
