using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProductCatalog.Entities;
using ProductCatalog.WebApi.Data;
using ProductCatalog.WebApi.DTOs;
using ProductCatalog.WebApi.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductCatalog.WebApi.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class ProductCatalogsController : ControllerBase
    {
        private readonly ILogger<ProductCatalogsController> _logger;
        private readonly IProductCatalogDbContext _context;
        private readonly IMapper _mapper;
        private readonly IExcelService _excelService;

        public ProductCatalogsController(IProductCatalogDbContext context, IExcelService excelService,
            IMapper mapper, ILogger<ProductCatalogsController> logger)
        {
            _logger = logger;
            _excelService = excelService;
            _mapper = mapper;
            _context = context;
        }

        [HttpGet("getall")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var products = await _context.Products.ToListAsync();
                var productsDto = _mapper.Map<List<ProductDto>>(products);

                if (productsDto == null)
                {
                    return NotFound();
                }
                return Ok(productsDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpectedd error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            try
            {
                var product = await _context.Products.FindAsync(new object[] { id });
                var productDto = _mapper.Map<ProductDto>(product);

                if (productDto == null)
                {
                    return NotFound();
                }
                return Ok(productDto);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpectedd error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            try
            {
                var product = await _context.Products.FindAsync(new object[] { id });

                if (product == null)
                {
                    return NotFound();
                }
                _context.Products.Remove(product);
                int result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return Ok(product);
                }
                return BadRequest("Cannot update properly");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpectedd error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> Update(long id, [FromBody] Product productData)
        {
            try
            {
                if (productData == null)
                {
                    await Task.CompletedTask;
                    throw new ArgumentException(nameof(productData));
                }

                var product = await _context.Products.FindAsync(new object[] { id });

                if (product == null)
                {
                    return NotFound();
                }
                else
                {
                    product.Code = productData.Code;
                    product.Name = productData.Name;
                    product.Price = productData.Price;
                    product.Photo = productData.Photo;
                    product.LastUpdated = DateTime.UtcNow;

                    int result = await _context.SaveChangesAsync();

                    if (result > 0)
                    {
                        return Ok(product);
                    }

                    return BadRequest("Cannot update properly");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpectedd error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] Product product)
        {
            try
            {
                if (product == null)
                {
                    await Task.CompletedTask;
                    throw new ArgumentException(nameof(product));
                }
                _context.Products.Add(product);
                int result = await _context.SaveChangesAsync();

                if (result > 0)
                {
                    return Ok(product);
                }
                return BadRequest("Cannot save properly");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpectedd error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("import")]
        public async Task<IActionResult> Import(IFormFile file)
        {
            if (file == null)
            {
                await Task.CompletedTask;
                throw new ArgumentException(nameof(file));
            }
            string fileName = file.FileName;

            try
            {
                var stream = _excelService.FileToStream(file);
                var excelReader = _excelService.InitExcelReader(fileName, stream);

                var list = new List<Product>();
                excelReader.Read();
                while (excelReader.Read())
                {
                    list.Add(new Product()
                    {
                        Code = excelReader.GetString(0),
                        Name = excelReader.GetString(1),
                        Price = Convert.ToDecimal(excelReader.GetDouble(2))
                    });
                }
                excelReader.Dispose();
                stream.Dispose();

                _context.Products.AddRange(list);
                int result = await _context.SaveChangesAsync();

                if (result == list.Count)
                {
                    return Ok();
                }
                return BadRequest("Cannot save properly");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Unexpectedd error: {ex.Message}");
                return BadRequest(ex.Message);
            }
        }
    }
}
