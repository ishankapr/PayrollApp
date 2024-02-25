using FetchEmployeeInfo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FetchEmployeeInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeInfoController : ControllerBase
    {
        private readonly ILogger<EmployeeInfoController> _logger;
        private IFetchDataService _fetchDataService;
        public EmployeeInfoController(ILogger<EmployeeInfoController> logger, IFetchDataService fetchDataService)
        {
            _logger = logger;
            _fetchDataService = fetchDataService;
        }

        [HttpGet, Route("getemployeeinfo")]
        [Authorize]
        async Task<ActionResult> GetEmployeeInfo()
        {
            try
            {
                var employees = await _fetchDataService.GetAllEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}
