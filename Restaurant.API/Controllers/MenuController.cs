using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.CustomActionFilter;
using Restaurant.Core.Services.Category.Interface;
using Restaurant.Core.Services.Menu.Interface;
using Restaurant.Data.Models.Menu;
using System.Net.Mime;
using System.Threading.Tasks;

namespace Restaurant.API.Controllers
{
    [AuthorizeActionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;
        private readonly ICategoryService _categoryService;
        public MenuController(IMenuService menuService, ICategoryService categoryService)
        {
            _menuService = menuService;
            _categoryService = categoryService;
        }

        [HttpPost, Route("addtomenu")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> Post(CreateMenuRequest createMenuRequest)
        {
            var categoryResult = await _categoryService.GetCategoryById(createMenuRequest.CategoryId);
            if (categoryResult == null) return NotFound($"There Is No Category With Id, {createMenuRequest.CategoryId}, In Our Records");
            var menuResult = await _menuService.IsDishExists(createMenuRequest.DishName);
            if (menuResult) return Conflict();

            if (await _menuService.CreateMenuItem(createMenuRequest)) return Ok();

            return Problem();
        }

        [HttpGet, Route("getmenu")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> Get()
        {
            var result = await _menuService.GetAllMenu();
            if (result is not null)
                return Ok(result);

            return NoContent();
        }
    }
}
