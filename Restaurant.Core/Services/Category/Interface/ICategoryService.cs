using Restaurant.Core.Helpers.Autofac;
using Restaurant.Data.Models.Category;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Restaurant.Core.Services.Category.Interface
{
    public interface ICategoryService : IAutoDependencyCore
    {
        /// <summary>
        /// Add Category To Table
        /// </summary>
        /// <param name="createCategoryRequest"></param>
        /// <returns></returns>
        Task<bool> AddCategory(CreateCategoryRequest createCategoryRequest);

        /// <summary>
        /// Checks If Category Already Exists
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        Task<bool> IsCategoryExists(string name);

        /// <summary>
        /// Returns All Categories
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<CategoryResponse>> GetCategories();
    }
}