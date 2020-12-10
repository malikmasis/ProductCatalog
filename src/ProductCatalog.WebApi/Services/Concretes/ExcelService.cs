using ExcelDataReader;
using Microsoft.AspNetCore.Http;
using ProductCatalog.WebApi.Models;
using ProductCatalog.WebApi.Services.Interfaces;
using System.IO;

namespace ProductCatalog.WebApi.Services.Concretes
{
    public class ExcelService : IExcelService
    {
        public MemoryStream FileToStream(IFormFile file)
        {
            long fileLength = file.Length;
            byte[] buffer = new byte[file.Length];
            if (fileLength > 0)
            {
                using (var ms = new MemoryStream())
                {
                    System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
                    file.CopyTo(ms);
                    buffer = ms.ToArray();
                }
            }

            var stream = new MemoryStream(buffer);
            return stream;
        }

        public IExcelDataReader InitExcelReader(string fileName, Stream stream)
        {
            IExcelDataReader excelReader = null;
            var extension = Path.GetExtension(fileName);

            if (extension == FileExtension.Excel2007)
            {
                excelReader = ExcelReaderFactory.CreateOpenXmlReader(stream);
            }
            else if (extension == FileExtension.Excel97And2003 || extension == FileExtension.Csv)
            {
                excelReader = ExcelReaderFactory.CreateBinaryReader(stream);
            }

            return excelReader;
        }
    }
}
