using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Restaurant.Core.Services.Order.Config;
using Restaurant.Core.Services.Order.Interface;
using Restaurant.Data.Models.Order;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;

namespace Restaurant.Core.Services.Order.Service
{
    public class OrderService : IOrderService
    {
        private readonly IRepository _repository;
        private readonly ILogger<OrderService> _logger;
        public OrderService(IRepository repository, ILogger<OrderService> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        public async Task<bool> AddOrder(CreateOrderRequest createOrderRequest)
        {
            IDbContextTransaction transaction = await _repository.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var batchId = $"ORD-{DateTime.Now.Ticks}";
                foreach (var item in createOrderRequest.OrderRequests)
                {
                    var newOrder = new Data.Entities.Order
                    {
                        BatchId = batchId,
                        CustomerId = createOrderRequest.CustomerId,
                        Quantity = item.Quantity                           
                    };

                    object[] primaryKeys = await _repository.InsertAsync(newOrder);

                    var orderId = (Guid)primaryKeys[0];
                    var menuDetails = new Data.Entities.OrderMenu
                    {
                         MenuId = item.MenuId,
                         OrderId = orderId
                    };

                    await _repository.InsertAsync(menuDetails);
                }

                await transaction.CommitAsync();

                return true;
            }
            catch (Exception)
            {
                await transaction.RollbackAsync();
                throw;
            }
        }

        public async Task<IEnumerable<OrderResponse>> GetOrdersByCustomer(string customerId)
        {
            try
            {
                _logger.LogInformation($"GetOrderByCustomer -----> Get Orders For {customerId} at {DateTime.Now}");
                var result = await _repository.GetListAsync(GetSpecification(customerId), true);
                return result?.ToOrderList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetOrderByCustomer Error -----> Get Orders Failed for {customerId}. {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<OrderResponse>> GetOrders()
        {
            try
            {
                _logger.LogInformation($"GetOrders -----> Get Orders at {DateTime.Now}");
                var result = await _repository.GetListAsync(GetSpecification(includeOrders: true));
                return result?.ToOrderList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetOrders Error -----> Get Orders Failed. {ex.Message}");
                return null;
            }
        }

        private static Specification<Data.Entities.Order> GetSpecification(string customerId = null, bool includeOrders = false)
        {
            var specification = new Specification<Data.Entities.Order>();
            if(!string.IsNullOrEmpty(customerId)) specification.Conditions.Add(e => e.Customer.Id == Guid.Parse(customerId));

            if (includeOrders) specification.Includes = query => query.Include(e => e.OrderMenus);
            specification.Skip = 0;
            specification.Take = 15;
            return specification;
        }
    }
}