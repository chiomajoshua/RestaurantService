using Microsoft.Extensions.Logging;
using Restaurant.Core.Services.Authentication.Interface;
using Restaurant.Data.Models.Authentication;
using System;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;

namespace Restaurant.Core.Services.Authentication.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IRepository _repository;
        private readonly ILogger<AuthenticationService> _logger;
        public AuthenticationService(IRepository repository, ILogger<AuthenticationService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<bool> Login(LoginRequest loginRequest)
        {
            try
            {
                _logger.LogInformation(message: $"Login -----> {loginRequest.Email} tried to logon at {DateTime.Now}");
                var userAccount = await _repository.GetAsync<Data.Entities.Customer>(x => x.EmailAddress == loginRequest.Email);

                if (userAccount is null) return false;

                return userAccount.EmailAddress == loginRequest.Email && BCrypt.Net.BCrypt.Verify(loginRequest.Password, userAccount.PasswordHash);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Login Error ----> Login Failed for user {loginRequest.Email}. {ex.Message}");
                return false;
            }
        }
    }
}