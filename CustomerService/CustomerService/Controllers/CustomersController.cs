using Microsoft.AspNetCore.Mvc;
using Application.Interfaces;
using Domain.Types;

namespace CustomerService.Controllers
{
    public class CustomersController : ControllerBase
    {
        private readonly ICustomerService _customerService;

        public CustomersController(ICustomerService orderService)
        {
            _customerService = orderService;
        }

        [HttpGet("validateCustomer")]
        public async Task<ActionResult> ValidateCustomer([FromQuery] ValidateCustomerRequest request)
        {
            try
            {
                var response = await _customerService.ValidateCustomerAsync(request);
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
}
