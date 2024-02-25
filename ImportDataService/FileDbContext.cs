using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using ImportDataService.Models;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace ImportDataService
{
    public class FileDbContext : DbContext
    {
        public DbSet<EmployeeInfo> Employees { get; set; }
        public DbSet<FileHistory> FileHistories { get; set; }

        public FileDbContext(DbContextOptions<FileDbContext> dbContextOptions) : base(dbContextOptions)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        
    }
}
