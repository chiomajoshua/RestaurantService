using Restaurant.Core.Helpers.Autofac;
using Restaurant.Data.Models.Menu;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Core.Services.Menu.Interface
{
    public interface IMenuService : IAutoDependencyCore
    {
        /// <summary>
        /// Add Menu To List
        /// </summary>
        /// <param name="createMenuRequest"></param>
        /// <returns></returns>
        Task<bool> CreateMenuItem(CreateMenuRequest createMenuRequest);


        /// <summary>
        /// Gets Complete Menu
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MenuResponse>> GetAllMenu();

        /// <summary>
        /// Checks if Dish is added to menu
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> IsDishExists(string name);
    }
}