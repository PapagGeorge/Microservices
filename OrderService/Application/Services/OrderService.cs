using Application.Interfaces;
using Domain.CustomerServiceTypes;
using Domain.Models;

public class OrderService : IOrderService
{
    private readonly ICustomerHttpClientRepository _customerRepository;
    private readonly IOrderRepository _orderRepository;

    public OrderService(ICustomerHttpClientRepository customerRepository, IOrderRepository orderRepository)
    {
        _customerRepository = customerRepository;
        _orderRepository = orderRepository;
    }

    public async Task<CreateNewOrderResponse> CreateNewOrderAsync(CreateNewOrderRequest request)
    {
        // Step 1: Validate customer
        var validationResponse = await _customerRepository.ValidateCustomerAsync(new ValidateCustomerRequest
        {
            CustomerId = request.CustomerId,
            ShippingAddressId = request.ShippingAddressId
        });

        if (!validationResponse.IsValid)
        {
            return new CreateNewOrderResponse
            {
                IsSuccess = false,
                Message = validationResponse.ValidationMessage
            };
        }

        // Step 2: Map request to domain models
        var order = new Order
        {
            Id = Guid.NewGuid(),
            CustomerId = request.CustomerId,
            OrderDate = DateTime.UtcNow,
            TotalAmount = 0, // Calculated later
            Status = OrderStatus.Pending,
            ShippingAddressId = request.ShippingAddressId,
            Products = request.Items.Select(item => new ProductQuantity
            {
                Id = Guid.NewGuid(),
                ProductId = item.ProductId,
                Quantity = item.Quantity
            }).ToList()
        };

        // Step 3: Calculate total amount (mocked for simplicity)
        order.TotalAmount = order.Products.Sum(p => p.Quantity * 10); // Assume each product costs 10 units

        // Step 4: Save order and associated products to database
        await _orderRepository.CreateOrderAsync(order);

        return new CreateNewOrderResponse
        {
            IsSuccess = true,
            Message = "Order created successfully.",
            OrderId = order.Id,
            Status = order.Status
        };
    }
}
