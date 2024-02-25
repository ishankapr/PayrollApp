using ImportDataService.Models;
using Microsoft.EntityFrameworkCore;

namespace ImportDataService.Services
{
    public class ImportService : IImportService
    {
        private readonly FileDbContext _fileContext;

        public ImportService(FileDbContext fileContext) 
        { 
            _fileContext = fileContext;
        }

        public async Task<int> ImportFile(IFormFile file)
        {
            //Because use of multiple tables same request, we use transaction to confirm both insertions
            using var transaction = _fileContext.Database.BeginTransaction();

            try { 

                if (file != null && file.Length > 0)
                {
                    List<EmployeeInfo> employees = new();

                    //Check the existancy for duplicate
                    var existingFile = await _fileContext.FileHistories.Where(x => x.FileName == file.FileName).FirstOrDefaultAsync();
                    if (existingFile != null)
                    {
                        return -1;
                    }

                    var fileHistory = new FileHistory()
                    {
                        FileName = file.FileName,
                        CreatedOn = DateTime.Now
                    };

                    _fileContext.FileHistories.Add(fileHistory);

                    using (var fileStream = file.OpenReadStream())
                    using (var reader = new StreamReader(fileStream))
                    {
                        reader.ReadLine();
                        while (reader.Peek() != -1)
                        {
                            var row = reader.ReadLine();
                            List<string> lineValues = row.Split(',').ToList();
                            employees.Add(new EmployeeInfo
                            {
                                EmployeeID = Convert.ToInt32(lineValues[0]),
                                DepartmentID = Convert.ToInt32(lineValues[1]),
                                Date = Convert.ToDateTime(lineValues[2]),
                                Amount = Convert.ToDecimal(lineValues[3]),
                                Status = lineValues[4]

                            });
                        }
                    }

                    _fileContext.Employees.AddRange(employees);
                     var result = await _fileContext.SaveChangesAsync();
                    transaction.Commit();
                    return result;
                }
            }
            catch (Exception ex)
            {
                transaction.Rollback();
                Console.WriteLine(ex.Message);
                return -1;
            }

            return 0;

        }
    }
}
