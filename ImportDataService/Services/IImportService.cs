namespace ImportDataService.Services
{
    public interface IImportService
    {
        public Task<int> ImportFile(IFormFile file);
    }
}
