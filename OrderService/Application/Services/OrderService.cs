using Application.Interfaces;
using Application.Services;
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
        request.Validate();

        // Step 1: Validate customer
        var validateCustomerRequest = new ValidateCustomerRequest();
        validateCustomerRequest.CustomerId = request.CustomerId;
        validateCustomerRequest.ShippingAddressId = request.ShippingAddressId;
        validateCustomerRequest.MobilePhone = request.MobilePhone;

        var validationResponse = await _customerRepository.ValidateCustomerAsync(validateCustomerRequest);

        if (!validationResponse.IsValid)
        {
            var newOrderFailedCustomerValidationResponse = new CreateNewOrderResponse();
            newOrderFailedCustomerValidationResponse.IsSuccess = false;
            newOrderFailedCustomerValidationResponse.Message = validationResponse.ValidationMessage;

            return newOrderFailedCustomerValidationResponse;
        }

        // Step 2: Map request to domain models
        var order = new Order();
        var orderId = Guid.NewGuid();
        order.Id = orderId;
        order.CustomerId = request.CustomerId;
        order.OrderDate = DateTime.UtcNow;
        order.Status = OrderStatus.Pending;
        order.ShippingAddressId = request.ShippingAddressId;
        order.Products = request.Items.Select(item => new ProductQuantity
        {
            Id = Guid.NewGuid(),
            ProductId = item.ProductId,
            OrderId = orderId,
            Quantity = item.Quantity,
            UnitPrice = item.UnitPrice
        }).ToList();

        order.TotalAmount = order.Products.Sum(p => p.Quantity * p.UnitPrice);

        // Step 4: Save order and associated products to database
        await _orderRepository.CreateOrderAsync(order);

        var response = new CreateNewOrderResponse();
        response.IsSuccess = true;
        response.Message = "Order created successfully.";
        response.OrderId = order.Id;
        response.Status = order.Status;
        return response;

    }
}
