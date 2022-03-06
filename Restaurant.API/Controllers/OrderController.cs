using Microsoft.AspNetCore.Mvc;
using Restaurant.API.CustomActionFilter;
using Restaurant.Core.Services.Menu.Interface;
using Restaurant.Core.Services.Order.Interface;

namespace Restaurant.API.Controllers
{
    [AuthorizeActionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly IOrderService _orderService;
        public OrderController(IMenuService menuService, IOrderService orderService)
        {
            _menuService = menuService;
            _orderService = orderService;
        }
    }
}
