using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Restaurant.API.CustomActionFilter;

namespace Restaurant.API.Controllers
{
    [AuthorizeActionFilter]
    [Route("api/[controller]")]
    [ApiController]
    public class MenuController : ControllerBase
    {
        public MenuController()
        {

        }
    }
}
