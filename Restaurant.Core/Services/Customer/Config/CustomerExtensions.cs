using Restaurant.Core.Services.Order.Config;
using Restaurant.Data.Models.Customer;

namespace Restaurant.Core.Services.Customer.Config
{
    public static class CustomerExtensions
    {
        public static Data.Entities.Customer ToDbCustomer(this CreateCustomerRequest createCustomerRequest)
        {
            return new Data.Entities.Customer
            {
                AccountStatus = true,
                EmailAddress = createCustomerRequest.EmailAddress,
                FirstName = createCustomerRequest.FirstName,
                LastName = createCustomerRequest.LastName,
                MiddleName = createCustomerRequest.MiddleName,
                PhoneNumber = createCustomerRequest.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(createCustomerRequest.Password)
            };
        }

        public static CustomerResponse ToCustomer(this Data.Entities.Customer customer)
        {
            return new CustomerResponse
            {                
                EmailAddress = customer.EmailAddress,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                MiddleName = customer.MiddleName,
                PhoneNumber = customer.PhoneNumber,
                CustomerId = customer.Id.ToString(),
                AccountStatus = customer.AccountStatus,
                Orders = customer.Orders.ToCustomerOrderList()
            };
        }
    }
}