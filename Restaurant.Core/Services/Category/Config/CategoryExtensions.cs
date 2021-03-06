using Restaurant.Data.Models.Category;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Core.Services.Category.Config
{
    public static class CategoryExtensions
    {
        public static Data.Entities.Category ToDbCategory(this CreateCategoryRequest createCategoryRequest)
        {
            return new Data.Entities.Category
            {
                 Name = createCategoryRequest.Name,
                 Description = createCategoryRequest?.Description
            };
        }

        public static IEnumerable<CategoryResponse> ToCategoryList(this List<Data.Entities.Category> categories)
        {
            var result = new List<CategoryResponse>();
            result.AddRange(categories.Select(data => new CategoryResponse()
            {
                  Description = data.Description,
                  Name = data.Name,
                  Id = data.Id
            }));
            return result;
        }

        public static CategoryResponse ToCategory(this Data.Entities.Category category)
        {
            return new CategoryResponse
            {
                Description = category.Description,
                Name = category.Name,
                Id = category.Id
            };
        }
    }
}