using Microsoft.Extensions.Logging;
using Restaurant.Core.Services.Category.Config;
using Restaurant.Core.Services.Category.Interface;
using Restaurant.Data.Models.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TanvirArjel.EFCore.GenericRepository;

namespace Restaurant.Core.Services.Category.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository _repository;
        private readonly ILogger<CategoryService> _logger;
        public CategoryService(IRepository repository, ILogger<CategoryService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public async Task<bool> AddCategory(CreateCategoryRequest createCategoryRequest)
        {
            try
            {
                _logger.LogInformation($"AddCategory -----> {createCategoryRequest.Name} at {DateTime.Now}");
                var response = await _repository.InsertAsync(createCategoryRequest.ToDbCategory());
                if (response is not null) return true;
                return false;
            }
            catch (Exception ex)
            {
                _logger.LogError($"AddCategory Error -----> Category Creation Failed for {createCategoryRequest.Name}. {ex.Message}");
                return false;
            }
        }

        public async Task<IEnumerable<CategoryResponse>> GetCategories()
        {
            var response = await _repository.GetListAsync(GetSpecification());
            return response.ToCategoryList();
        }

        public async Task<bool> IsCategoryExists(string name)
        {
            try
            {
                _logger.LogInformation($"IsCategoryExists -----> Category Exist Check for {name} at {DateTime.Now}");
                return await _repository.ExistsAsync<Data.Entities.Category>(x => x.Name == name);
            }
            catch (Exception ex)
            {
                _logger.LogError($"IsCategoryExists Error -----> Category Exist Check Failed for {name}. {ex.Message}");
                return false;
            }
        }

        private static Specification<Data.Entities.Category> GetSpecification()
        {
            var specification = new Specification<Data.Entities.Category>
            {
                OrderBy = q => q.OrderBy(e => e.Name),
                Skip = 0,
                Take = 20
            };
            return specification;
        }
    }
}