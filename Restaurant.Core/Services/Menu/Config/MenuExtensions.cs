using Restaurant.Data.Models.Menu;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Core.Services.Menu.Config
{
    public static class MenuExtensions
    {
        public static Data.Entities.Menu ToDbMenu(this CreateMenuRequest createMenuRequest)
        {
            return new Data.Entities.Menu
            {
                CategoryId = createMenuRequest.CategoryId,
                DishName = createMenuRequest.DishName,
                Description = createMenuRequest.Description,
                Price = createMenuRequest.Price
            };
        }

        public static IEnumerable<MenuResponse> ToMenuList(this IEnumerable<Data.Entities.Menu> menuData)
        {
            var result = new List<MenuResponse>();
            result.AddRange(menuData.Select(data => new MenuResponse()
            {
                Category = data.Category.Name,
                Description = data.Description,
                DishName = data.DishName,
                Price = data.Price,
                CategoryId = data.CategoryId,
                MenuId = data.Id
            }));
            return result;
        }

        public static MenuResponse ToMenu(this Data.Entities.Menu menuData)
        {
            return new MenuResponse
            {
                Category = menuData.Category.Name,
                Description = menuData.Description,
                DishName = menuData.DishName,
                Price = menuData.Price,
                CategoryId = menuData.CategoryId,
                MenuId = menuData.Id
            };
        }
    }
}