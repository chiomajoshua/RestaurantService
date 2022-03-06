using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.CustomActionFilter;
using Restaurant.Core.Services.Customer.Interface;
using Restaurant.Core.Services.Order.Interface;
using Restaurant.Data.Models.Order;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Restaurant.API.Controllers
{
    [AuthorizeActionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;


        public OrderController(IOrderService orderService, ICustomerService customerService)
        {
            _orderService = orderService;
            _customerService = customerService;
        }

        [HttpPost, Route("neworder")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Post(CreateOrderRequest createOrderRequest)
        {
            var customerResult = await _customerService.GetCustomerByIdAsync(createOrderRequest.CustomerId);
            if (customerResult is null) return NotFound($"Customer Does Not Exist In Our Records");

            var orderResult = await _orderService.AddOrder(createOrderRequest);
            
            if(!string.IsNullOrEmpty(orderResult))
                return Ok(orderResult);

            return Problem("We Could Not Add Your Order. Please Confirm Selected Menu Item Is In Our List");
        }

        [HttpGet, Route("getorders")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {
            var result = await _orderService.GetOrders();
            if (result is not null)
                return Ok(result);

            return NoContent();
        }

        [HttpGet, Route("getordersbyCustomer")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(string customerId)
        {
            var result = await _orderService.GetOrdersByCustomer(customerId);
            if (result is not null)
                return Ok(result);

            return NoContent();
        }
    }
}