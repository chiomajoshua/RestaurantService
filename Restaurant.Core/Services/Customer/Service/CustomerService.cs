using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Core.Services.Customer.Config;
using Restaurant.Core.Services.Customer.Interface;
using Restaurant.Data.Models.Customer;
using System;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;

namespace Restaurant.Core.Services.Customer.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly IRepository _repository;
        private readonly ILogger<CustomerService> _logger;
        public CustomerService(IRepository repository, ILogger<CustomerService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Guid> CreateCustomerAsync(CreateCustomerRequest createCustomerRequest)
        {
            try
            {
                _logger.LogInformation($"CreateCustomer -----> {createCustomerRequest.EmailAddress} tried to create an account at {DateTime.Now}");
                var response = await _repository.InsertAsync(createCustomerRequest.ToDbCustomer());
                if (response is not null) return (Guid)response[0];
                return Guid.Empty;
            }
            catch (Exception ex)
            {
                _logger.LogError($"IsCustomerExists Error -----> Account Creation Failed for {createCustomerRequest}. {ex.Message}");
                return Guid.Empty;
            }
        }

        public async Task<CustomerResponse> GetCustomerByEmailAsync(string email)
        {
            try
            {
                _logger.LogInformation($"GetCustomerByEmailAsync -----> Account Check for {email} at {DateTime.Now}");
                var result = await _repository.GetAsync(GetSpecification(email), true);
                return result?.ToCustomer();
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetCustomerByEmailAsync Error -----> Account Check Failed for {email}. {ex.Message}");
                return null;
            }
        }

        public async Task<CustomerResponse> GetCustomerByIdAsync(Guid id)
        {
            try
            {
                _logger.LogInformation($"GetCustomerByIdAsync -----> Account Check for {id} at {DateTime.Now}");
                var result = await _repository.GetAsync<Data.Entities.Customer>(x => x.Id == id);
                return result?.ToCustomer();
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetCustomerByIdAsync Error -----> Account Check Failed for {id}. {ex.Message}");
                return null;
            }
        }

        public async Task<bool> IsCustomerExistsAsync(string email)
        {
            try
            {
                _logger.LogInformation($"IsCustomerExists -----> Account Exist Check for {email} at {DateTime.Now}");
                return await _repository.ExistsAsync<Data.Entities.Customer>(x => x.EmailAddress == email);
            }
            catch (Exception ex)
            {
                _logger.LogError($"IsCustomerExists Error -----> Account Exist Check Failed for {email}. {ex.Message}");
                return false;
            }
        }


        private static Specification<Data.Entities.Customer> GetSpecification(string email, bool includeOrders = false)
        {
            var specification = new Specification<Data.Entities.Customer>();
            specification.Conditions.Add(e => e.EmailAddress == email);

            if(includeOrders) specification.Includes = query => query.Include(e => e.Orders);
            specification.Skip = 0;
            specification.Take = 15;
            return specification;
        }
    }
}