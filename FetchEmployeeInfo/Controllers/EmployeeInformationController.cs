using FetchEmployeeInfo.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FetchEmployeeInfo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeInformationController : ControllerBase
    {

        private readonly ILogger<EmployeeInformationController> _logger;
        private IFetchDataService _fetchDataService;
        public EmployeeInformationController(ILogger<EmployeeInformationController> logger, IFetchDataService fetchDataService)
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