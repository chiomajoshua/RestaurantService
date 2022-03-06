using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Services.Customer.Interface;
using Restaurant.Data.Models.Customer;
using System.Net.Mime;
using System.Threading.Tasks;


namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OnboardingController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        public OnboardingController(ICustomerService customerService)
        {
            _customerService = customerService;
        }

        [HttpPost, Route("onboardcustomer")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post(CreateCustomerRequest createCustomerRequest)
        {
            var customerResult = await _customerService.IsCustomerExistsAsync(createCustomerRequest.EmailAddress);
            if (customerResult) return Conflict();

            if (await _customerService.CreateCustomerAsync(createCustomerRequest)) return Ok();

            return Problem();
        }
    }
}