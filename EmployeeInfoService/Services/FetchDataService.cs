using EmployeeInfoService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EmployeeInfoService.Services
{
    public class FetchDataService : IFetchDataService
    {
        private readonly EmployeeDbContext _employeeDbContext;

        public FetchDataService(EmployeeDbContext employeeDbContext) 
        {
            _employeeDbContext = employeeDbContext;
        }

        [HttpGet("tasks")]
        public async Task<List<EmployeeInfo>> GetAllEmployees()
        {
            return await _employeeDbContext.Employees.ToListAsync();
        }
    }
}
