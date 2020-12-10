using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using ProductCatalog.Entities;
using ProductCatalog.WebApi.Controllers;
using ProductCatalog.WebApi.Data;
using ProductCatalog.WebApi.DTOs;
using ProductCatalog.WebApi.Mapping;
using ProductCatalog.WebApi.Services.Interfaces;
using System.Collections.Generic;
using Xunit;

namespace ProductCatalog.UnitTest.Controllers
{
    public class ProductCatalogsControllerTest
    {
        [Fact]
        public void TestGetAll()
        {
            var mockedExcelService = new Mock<IExcelService>();
            var mockedLog = new Mock<ILogger<ProductCatalogsController>>();

            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new OrganizationProfile());
            });
            var mapper = mockMapper.CreateMapper();

            var options = new DbContextOptionsBuilder<ProductCatalogDbContext>()
            .UseInMemoryDatabase(databaseName: "ProductListDatabase")
            .Options;

            using (var context = new ProductCatalogDbContext(options))
            {
                context.Products.Add(new Product { Id = 1, Code = "Code 1", Price = 218, Name = "Name 1" });
                context.Products.Add(new Product { Id = 2, Code = "Code 2", Price = 217, Name = "Name 2" });
                context.SaveChanges();
            }

            using (var context = new ProductCatalogDbContext(options))
            {
                var controller = new ProductCatalogsController(context, mockedExcelService.Object, mapper, mockedLog.Object);

                var productsDto = (List<ProductDto>)((OkObjectResult)controller.GetAll().Result).Value;

                Assert.Equal(2, productsDto.Count);
            }

        }
    }
}
