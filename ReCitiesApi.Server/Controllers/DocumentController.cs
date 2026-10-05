using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReCitiesApi.Server.Services;
using System.Security.Claims;

namespace ReCitiesApi.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/documents")]
    public class DocumentController (IDocumentService documentService): Controller
    {
        private readonly IDocumentService _documentService = documentService;

        [HttpGet]
        [Route("user-structure")]
        public async Task<IActionResult> GetUserStructure()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var folderDto = await _documentService.GetUserStructureAsync(userId);
            return Ok(folderDto);
        }
    }
}
