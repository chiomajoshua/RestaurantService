using Microsoft.EntityFrameworkCore.Storage;
using Restaurant.Core.Services.Order.Interface;
using Restaurant.Data.Models.Order;
using System;
using System.Data;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;

namespace Restaurant.Core.Services.Order.Service
{
    public class OrderService : IOrderService
    {
        private readonly IRepository _repository;
        public OrderService(IRepository repository)
        {
            _repository = repository;
        }


        public async Task<bool> AddOrder(CreateOrderRequest createOrderRequest)
        {
            IDbContextTransaction transaction = await _repository.BeginTransactionAsync(IsolationLevel.ReadCommitted);
            try
            {
                var batchId = $"ORD-{DateTime.Now.Ticks}";
                foreach (var item in createOrderRequest.OrderRequest)
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
    }
}