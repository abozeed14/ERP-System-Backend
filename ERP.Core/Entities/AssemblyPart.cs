using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Core.Entities
{
    public class AssemblyPart
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public required string Name { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
        public int ReorderLevel { get; set; }
        [Range(0.01, double.MaxValue, ErrorMessage = "Purchase price must be greater than 0.")]
        public decimal PurchasePrice { get; set; }
        [NotMapped]
        public decimal TotalCost => Quantity * PurchasePrice;
        public virtual ICollection<ProductAssemblyPart> ProductAssemblyParts { get; set; } = new List<ProductAssemblyPart>();


    }
}
