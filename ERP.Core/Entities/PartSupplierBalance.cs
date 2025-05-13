using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ERP.Core.Entities
{
    public class PartSupplierBalance
    {
        [Key]
        public int PartSupplierID { get; set; } // Foreign key to  PartSupplier
        public decimal InitialBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public virtual PartSupplier PartSupplier { get; set; }
        [NotMapped]
        public decimal FinalBalance => InitialBalance + CurrentBalance;
    }
}
