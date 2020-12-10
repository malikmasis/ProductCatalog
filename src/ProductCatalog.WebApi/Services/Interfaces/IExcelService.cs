using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace ProductCatalog.WebApi.Services.Interfaces
{
    public interface IExcelService
    {
        MemoryStream FileToStream(IFormFile file);

        IExcelDataReader InitExcelReader(string fileName, Stream stream);
    }
}
