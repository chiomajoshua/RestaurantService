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

        public async Task<bool> IsDishExists(string name)
        {
            try
            {
                _logger.LogInformation($"IsDishExists -----> Dish Exist Check for {name} at {DateTime.Now}");
                return await _repository.ExistsAsync<Data.Entities.Menu>(x => x.DishName.Trim().ToLower() == name.Trim().ToLower());
            }
            catch (Exception ex)
            {
                _logger.LogError($"IsDishExists Error -----> Dish Exist Check Failed for {name}. {ex.Message}");
                return false;
            }
        }

        public async Task<MenuResponse> GetMenuById(Guid menuId)
        {
            try
            {
                _logger.LogInformation($"GetMenuById -----> Menu Check for {menuId} at {DateTime.Now}");
                var result = await _repository.GetListAsync(GetSpecification());
                return result.FirstOrDefault(x => x.Id == menuId).ToMenu();
            }
            catch (Exception ex)
            {
                _logger.LogError($"GetMenuById Error -----> Menu Check Failed for {menuId}. {ex.Message}");
                return null;
            }
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