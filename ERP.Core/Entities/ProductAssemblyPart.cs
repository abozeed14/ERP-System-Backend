using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ERP.Core.Entities
{
    public class ProductAssemblyPart
    {
      

        [Required]
        public int ProductID { get; set; } // Assembled Product ID

        [ForeignKey(nameof(ProductID))]
        public virtual Product Product { get; set; }

        [Required]
        public int AssemblyPartID { get; set; } // The part used

        [ForeignKey(nameof(AssemblyPartID))]
        public virtual AssemblyPart AssemblyPart { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }
    }
}
