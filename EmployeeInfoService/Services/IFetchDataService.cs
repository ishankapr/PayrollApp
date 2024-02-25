using EmployeeInfoService.Models;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeInfoService.Services
{
    public interface IFetchDataService
    {
        public Task<List<EmployeeInfo>> GetAllEmployees();
    }
}
