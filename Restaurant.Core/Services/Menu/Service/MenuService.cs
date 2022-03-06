using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Restaurant.Core.Services.Menu.Config;
using Restaurant.Core.Services.Menu.Interface;
using Restaurant.Data.Models.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;

namespace Restaurant.Core.Services.Menu.Service
{
    public class MenuService : IMenuService
    {
        private readonly IRepository _repository;
        private readonly ILogger<MenuService> _logger;
        public MenuService(IRepository repository, ILogger<MenuService> logger)
        {
            _logger = logger;
            _repository = repository;
        }

        public async Task<bool> CreateMenuItem(CreateMenuRequest createMenuRequest)
        {
            _logger.LogInformation($"CreateMenuItem -----> Adding DishName, {createMenuRequest.DishName}, to Menu at {DateTime.Now}");
            var response = await _repository.InsertAsync(createMenuRequest.ToDbMenu());
            if (response is not null) return true;

            return false;
        }

        public async Task<IEnumerable<MenuResponse>> GetAllMenu()
        {            
            var response = await _repository.GetListAsync(GetSpecification());
            return response.ToMenuList();
        }

        private static Specification<Data.Entities.Menu> GetSpecification()
        {
            var specification = new Specification<Data.Entities.Menu>
            {
                Includes = query => query.Include(e => e.Category),
                OrderBy = q => q.OrderBy(e => e.DishName),
                Skip = 0,
                Take = 20
            };
            return specification;
        }
    }
}