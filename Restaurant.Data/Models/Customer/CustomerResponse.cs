using Restaurant.Data.Models.Order;
using System.Collections.Generic;

namespace Restaurant.Data.Models.Customer
{
    public class CustomerResponse
    {
        public string CustomerId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public bool AccountStatus { get; set; }
        public IEnumerable<OrderResponse> Orders { get; set; }
    }
}