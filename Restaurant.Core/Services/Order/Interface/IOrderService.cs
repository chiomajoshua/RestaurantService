using Restaurant.Core.Helpers.Autofac;
using Restaurant.Data.Models.Order;
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
        Task<bool> AddOrder(CreateOrderRequest createOrderRequest);



    }
}