using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrdersController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    [HttpPost("createNewOrder")]
    public async Task<ActionResult> CreateNewOrder([FromBody] CreateNewOrderRequest request)
    {
        try
        {
            var response = await _orderService.CreateNewOrderAsync(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            // Log the exception (if logging is configured)
            return StatusCode(500, new
            {
                success = false,
                message = "An unexpected error occurred."
            });
        }
    }
}
