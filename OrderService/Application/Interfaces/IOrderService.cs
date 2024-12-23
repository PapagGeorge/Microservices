namespace Application.Interfaces;

public interface IOrderService
{
    Task<CreateNewOrderResponse> CreateNewOrderAsync(CreateNewOrderRequest request);
}