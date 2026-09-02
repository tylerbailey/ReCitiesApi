using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReCitiesApi.Server.Services;

namespace ReCitiesApi.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/neighborhood")]
    public class NeighborhoodController(INeighborhoodService neighborhoodService) : Controller
    {
        private readonly INeighborhoodService _neighborhoodService = neighborhoodService;

        [Route("all")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetNeighborhoodsAsync()
        {
            var neighborhoods = await _neighborhoodService.GetAllNeighborhoodsAsync();
            return Ok(neighborhoods);
        }

        [Route("{id}")]
        [HttpGet]
        public async Task<IActionResult> GetNeighborhoodByIdAsync(int id)
        {
            var neighborhood = await _neighborhoodService.GetNeighborhoodByIdAsync(id);
            if (neighborhood == null)
            {
                return NotFound();
            }
            return Ok(neighborhood);
        }
    }
}
