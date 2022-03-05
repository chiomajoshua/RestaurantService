using Restaurant.Data.Models.Order;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Core.Services.Order.Config
{
    public static class OrderExtensions
    {
        public static IEnumerable<OrderResponse> ToOrderList(this List<Data.Entities.Order> orderData)
        {
            var result = new List<OrderResponse>();
            result.AddRange(orderData.Select(data => new OrderResponse()
            {
                Id = data.Id,
                OrderDate = data.CreatedAt,
                Quantity = data.Quantity,
                BatchId = data.BatchId,
                DishName = data.OrderMenus.Where(x => x.OrderId == data.Id).Select(x => x.Menu.DishName).FirstOrDefault()
            }));
            return result;
        }

        public static IEnumerable<OrderResponse> ToCustomerOrderList(this ICollection<Data.Entities.Order> orderData)
        {
            var result = new List<OrderResponse>();
            result.AddRange(orderData.Select(data => new OrderResponse()
            {
                Id = data.Id,
                OrderDate = data.CreatedAt,
                Quantity = data.Quantity,
                BatchId = data.BatchId,
                DishName = data.OrderMenus.Where(x => x.OrderId == data.Id).Select(x => x.Menu.DishName).FirstOrDefault()
            }));
            return result;
        }
    }
}