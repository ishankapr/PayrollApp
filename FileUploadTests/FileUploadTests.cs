using Castle.Core.Logging;
using EmployeeInfoService;
using ImportDataService.Controllers;
using ImportDataService.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace FileUploadTests
{
    public class FileUploadTests
    {
        private readonly string existingFilePath;

        public FileUploadTests()
        {
            existingFilePath = "D:\\PayrollApp\\Employee Alowances.csv";
        }

        [Fact]
        public async Task UploadFile_ValidFile_ReturnsOkResult()
        {
            var context = new Mock<EmployeeDbContext>();
            var importService = new Mock<IImportService>(context.Object);
            var logger = new Mock<ILogger<FileImportController>>();
            // Arrange
            var controller = new FileImportController(logger.Object, importService.Object);

            if (File.Exists(existingFilePath))
            {
                using (var stream = new FileStream(existingFilePath, FileMode.Open))
                {
                    var file = new FormFile(stream, 0, stream.Length, "file", "Employee Alowances.csv");

                    // Act
                    var result = await controller.ImportFile(file);

                    // Assert
                    var okResult = Assert.IsType<OkObjectResult>(result);
                    Assert.Equal("File uploaded successfully", okResult.Value);

                    // Additional assertions or verifications based on your specific requirements
                }
            }
            else
            {
                throw new FileNotFoundException($"File not found: {existingFilePath}");
            }
        }
    }
}