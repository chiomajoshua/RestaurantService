using Restaurant.Core.Helpers.Autofac;
using Restaurant.Data.Models.Category;
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
    }
}