using ImportDataService.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImportDataService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileImportController : ControllerBase
    {

        private readonly ILogger<FileImportController> _logger;
        private readonly IImportService _importService;
        public FileImportController(ILogger<FileImportController> logger, IImportService importService)
        {
            _logger = logger;
            _importService = importService;
        }

        [HttpPost, Route("importfile")]
        [Authorize]
        public async Task<IActionResult> ImportFile(IFormFile file)
        {
            try
            {
                var result = await _importService.ImportFile(file);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}