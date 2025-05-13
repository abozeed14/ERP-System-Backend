using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace ERP.Core.Entities
{
    public class SupplierBalance
    {
        [Key]
        public int SupplierID { get; set; } // Foreign key to Supplier
        public decimal InitialBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public virtual Supplier Supplier { get; set; }
        [NotMapped]
        public decimal FinalBalance => InitialBalance + CurrentBalance;
    }
}
