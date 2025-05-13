using System.ComponentModel.DataAnnotations;
namespace ERP.Core.Entities
{
    public class ProductCategory
    {
        [Key]
        public int ProductCategoryId { get; set; }
        public required string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
    }
        
    
}
