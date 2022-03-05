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
        Task<Guid> CreateMenuItem(CreateMenuRequest createMenuRequest);


        /// <summary>
        /// Gets Complete Menu
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<MenuResponse>> GetAllMenu();
    }
}