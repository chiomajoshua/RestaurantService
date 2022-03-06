using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.CustomActionFilter;
using Restaurant.Core.Services.Customer.Interface;
using System;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Restaurant.API.Controllers
{
    [AuthorizeActionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public CustomerController(ICustomerService customerService)
        {
            _customerService = customerService;
        }


        [HttpGet, Route("getcustomerbyemail")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(string email)
        {
            var result = await _customerService.GetCustomerByEmailAsync(email);
            if (result is not null)
                return Ok(result);

            return NoContent();
        }

        [HttpGet, Route("getcustomerbyid")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get(Guid customerId)
        {
            var result = await _customerService.GetCustomerByIdAsync(customerId);
            if (result is not null)
                return Ok(result);

            return NoContent();
        }
    }
}