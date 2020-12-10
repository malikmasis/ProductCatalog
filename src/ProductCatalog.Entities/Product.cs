using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Entities
{
    public class Product : BaseEntity
    {
        public string Code { get; set; }
        public string Name { get; set; }
        [Range(0.0, 1000.0, ErrorMessage = "The field {0} must be greater than {1} and less than {2}")]
        public decimal Price { get; set; }
        public byte[] Photo { get; set; }
    }
}
