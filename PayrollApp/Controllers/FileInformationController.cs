using FetchDataService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PayrollApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileInformationController : ControllerBase
    {

        private readonly ILogger<FileInformationController> _logger;
        private readonly EmployeeDbContext _employeeDbContext;

        public FileInformationController(ILogger<FileInformationController> logger, EmployeeDbContext employeeDbContext)
        {
            _logger = logger;
            _employeeDbContext = employeeDbContext;
        }

        [HttpGet("getfileinfo")]
        [Authorize]
        async Task<ActionResult> GetFIleInfo()
        {
            var employees = await _employeeDbContext.Employees.ToListAsync();
            return Ok(employees);
        }
    }
}