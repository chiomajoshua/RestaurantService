using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.Core.Services.Authentication.Interface;
using Restaurant.Core.Services.Customer.Interface;
using Restaurant.Core.Services.Token.Interface;
using Restaurant.Data.Models.Authentication;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Restaurant.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ICustomerService _customerService;
        private readonly IAuthenticationService _authService;
        private readonly ITokenService _tokenService;
        public AuthController(ICustomerService customerService, ITokenService tokenService, IAuthenticationService authService)
        {
            _customerService = customerService;
            _authService = authService;
            _tokenService = tokenService;
        }

        [HttpPost, Route("login")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Post(LoginRequest loginRequest)
        {
            var userData = await _customerService.GetCustomerByEmailAsync(loginRequest.Email);

            if (userData is null) return NotFound();

            if (!userData.AccountStatus) return Unauthorized("Account is locked!. Please contact administrator");

            if (await _authService.Login(loginRequest))
            {
                var token = await _tokenService.CreateToken(userData.CustomerId);
                if (token == "----") return Unauthorized("Could not generate token. Please contact administrator.");
                return Ok(new LoginResponse { Token = token, Email = userData.EmailAddress });
            }
            return Unauthorized("Invalid login credentials");
        }


        [HttpPost, Route("Logout")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        public async Task<IActionResult> Post(string customerId)
        {
            if (string.IsNullOrEmpty(customerId)) return BadRequest("Request is empty");

            if (await _tokenService.DestroyToken(customerId))
                return Ok("Logout Successful");
            return Forbid("Logout Unsuccessful");
        }
    }
}
