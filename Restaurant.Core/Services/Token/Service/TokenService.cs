using Microsoft.Extensions.Logging;
using Restaurant.Core.Services.Customer.Interface;
using Restaurant.Core.Services.Token.Config;
using Restaurant.Core.Services.Token.Interface;
using Restaurant.Data.Models.Authentication;
using System;
using System.Linq;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;

namespace Restaurant.Core.Services.Token.Service
{
    public class TokenService : ITokenService
    {
        private readonly ILogger<TokenService> _logger;
        private readonly IRepository _repository;
        private readonly ICustomerService _customerService;
        public TokenService(IRepository repository, ILogger<TokenService> logger, ICustomerService customerService)
        {
            _repository = repository;
            _logger = logger;
            _customerService = customerService;
        }

        public async Task<string> CreateToken(Guid customerId)
        {
            try
            {
                var token = Helpers.Extensions.Encrypt($"{customerId}+{Guid.NewGuid().ToString().Replace("-", "")}+{DateTime.Now.Ticks}+{DateTime.Now.AddMinutes(60)}");
                var result = await SaveToken(new CreateTokenRequest { Token = token, CustomerId = customerId });
                return result ? token : "----";
            }
            catch (Exception ex)
            {
                _logger.LogError($"CreateToken Error ----> Token Creation Failed for user {customerId}. {ex.Message}");
                return "----";
            }
        }

        public async Task<bool> ValidateToken(string token)
        {
            if (string.IsNullOrEmpty(token)) return false;
            var request = Helpers.Extensions.Decrypt(token).Split('+');
            try
            {
                var customerRecords = await _customerService.GetCustomerByIdAsync(Guid.Parse(request.FirstOrDefault()));
                if (customerRecords is null) return false;

                return await _repository.ExistsAsync<Data.Entities.TokenLog>(x => x.CustomerId == Guid.Parse(request.FirstOrDefault()) && 
                                                                        x.Token == token &&
                                                                        DateTime.Now <= x.ExpiresAt &&
                                                                        x.IsActive == true);               
            }
            catch (Exception ex)
            {
                _logger.LogError($"ValidateToken Error ----> Token Validation Failed for user {request.FirstOrDefault()}. {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DestroyToken(string token)
        {
            try
            {
                var request = Helpers.Extensions.Decrypt(token).Split('+');
                var tokenLog = await _repository.GetListAsync<Data.Entities.TokenLog>(x => x.CustomerId == Guid.Parse(request.FirstOrDefault()) && x.IsActive);
                if(!tokenLog.Any()) return true;
                tokenLog.ForEach(x => x.IsActive = false && x.UpdatedAt == DateTimeOffset.Now);
                await _repository.UpdateAsync<Data.Entities.TokenLog>(tokenLog);

                var activeToken = await _repository.GetListAsync<Data.Entities.TokenLog>(x => x.CustomerId == Guid.Parse(request.FirstOrDefault()) && x.IsActive);
                if (activeToken.Any()) return false;
                return true;
            }
            catch(Exception ex)
            {
                _logger.LogError($"DestroyToken Error ----> Failed to destroy Token {ex.Message}");
                return false;
            }
        }

        private async Task<bool> SaveToken(CreateTokenRequest tokenRequest)
        {
            try
            {
                var response = await _repository.InsertAsync(tokenRequest.ToDbToken());
                if ((Guid)response[0] != Guid.Empty)
                    return true;
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"SaveToken Error ----> Failed to save Token {ex.Message}");
                return false;
            }
        }
    }
}