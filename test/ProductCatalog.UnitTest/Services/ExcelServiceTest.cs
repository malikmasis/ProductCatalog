using Microsoft.AspNetCore.Http;
using ProductCatalog.WebApi.Services.Concretes;
using System.IO;
using Xunit;

namespace ProductCatalog.UnitTest.Services
{
    public class ExcelServiceTest
    {
        [Fact]
        public void TestFileStreamAndInitExcelReader()
        {
            var byteArray = File.ReadAllBytes(@"..\..\..\Files\ImportProduct.xlsx");

            MemoryStream stream = new MemoryStream();
            stream.Write(byteArray, 0, byteArray.Length);
            IFormFile file = new FormFile(stream, 0, byteArray.Length, "file", "ImportProduct.xlsx");
           
            var excelService = new ExcelService();
            var ms = excelService.FileToStream(file);
            var reader = excelService.InitExcelReader("ImportProduct.xlsx", ms);

            Assert.Equal(3,reader.RowCount);
        }
    }
}
