using FetchEmployeeInfo.Models;
using Microsoft.AspNetCore.Mvc;

namespace FetchEmployeeInfo.Services
{
    public interface IFetchDataService
    {
        public Task<List<EmployeeInfo>> GetAllEmployees();
    }
}
