using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ReCitiesApi.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/documents")]
    public class DocumentController : Controller
    {
        public IActionResult GetUserStructure()
        {
            return Ok();
        }
    }
}
