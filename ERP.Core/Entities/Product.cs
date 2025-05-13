using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Core.Entities
{
    public class Product
    {
        [Key]
        public int ProductID { get; set; }

        [Required(ErrorMessage = "The product name is required.")]
        [StringLength(100, ErrorMessage = "The product name cannot exceed 100 characters.")]
        public required string Name { get; set; }

        [StringLength(500, ErrorMessage = "The description cannot exceed 500 characters.")]
        public string? Description { get; set; }

       
        [Range(0.01, double.MaxValue, ErrorMessage = "Selling price must be greater than 0.")]
        public decimal SellingPrice { get; set; }

       
        [Range(0.01, double.MaxValue, ErrorMessage = "Purchase price must be greater than 0.")]
        public decimal PurchasePrice { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int Quantity { get; set; } = 0;  
        [Range(0, int.MaxValue, ErrorMessage = "Quantity cannot be negative.")]
        public int InventoryQuantity { get; set; } = 0;
        [NotMapped]
        public int TotalQuantity => Quantity + InventoryQuantity;

        [Required(ErrorMessage = "Reorder level is required.")]
        [Range(0, int.MaxValue, ErrorMessage = "Reorder level cannot be negative.")]
        public int ReorderLevel { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        public bool IsActive { get; set; } = true; // To soft-delete products
        [Required]
        public bool IsAssembled { get; set; } = false; // Indicates if the product is made from components

        //Navigation Properties
        public int? SupplierID { get; set; }

        [ForeignKey(nameof(SupplierID))]
        public virtual Supplier Supplier { get; set; }

        [Required(ErrorMessage = "Product category is required.")]
        public int ProductCategoryID { get; set; }

        [ForeignKey(nameof(ProductCategoryID))]
        public virtual ProductCategory ProductCategory { get; set; }

        public int? FactoryID { get; set; }

        [ForeignKey(nameof(FactoryID))]
        public virtual Factory Factory { get; set; }
        // New Properties
        public virtual ICollection<ProductAssemblyPart> ProductAssemblyParts { get; set; } = new List<ProductAssemblyPart>();
    }
}