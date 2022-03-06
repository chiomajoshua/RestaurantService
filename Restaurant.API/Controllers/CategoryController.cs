using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.CustomActionFilter;
using Restaurant.Core.Services.Category.Interface;
using Restaurant.Data.Models.Category;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Restaurant.API.Controllers
{
    [AuthorizeActionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        
        [HttpPost, Route("addcategory")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post(CreateCategoryRequest createCategoryRequest)
        {
            var categoryResult = await _categoryService.IsCategoryExists(createCategoryRequest.Name);
            if (categoryResult) return Conflict();

            if (await _categoryService.AddCategory(createCategoryRequest)) return Ok();

            return Problem();
        }

        [HttpGet, Route("getcategories")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {
            var result = await _categoryService.GetCategories();
            if (result is not null)
                return Ok(result);

            return NoContent();
        }
    }
}