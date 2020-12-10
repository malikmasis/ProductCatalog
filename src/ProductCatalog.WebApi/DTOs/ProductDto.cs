namespace ProductCatalog.WebApi.DTOs
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
