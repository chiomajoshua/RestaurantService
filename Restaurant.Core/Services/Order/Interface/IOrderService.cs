using Restaurant.Core.Helpers.Autofac;
using Restaurant.Data.Models.Order;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Core.Services.Order.Interface
{
    public interface IOrderService : IAutoDependencyCore
    {
        /// <summary>
        /// Add Orders To Table
        /// </summary>
        /// <param name="createOrderRequest"></param>
        /// <returns></returns>
        Task<string> AddOrder(CreateOrderRequest createOrderRequest);

        /// <summary>
        /// Get All Orders
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<OrderResponse>> GetOrders();

        /// <summary>
        /// Get Orders for Single Customer
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        Task<IEnumerable<OrderResponse>> GetOrdersByCustomer(string customerId);
    }
}