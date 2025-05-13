using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ERP.Core.Entities
{
    public class CustomerBalance
    {
        [Key]
        public int CustomerID { get; set; } // Foreign key to Customer
        public decimal InitialBalance { get; set; }
        public decimal CurrentBalance { get; set; }
        public virtual Customer Customer { get; set; }
        [NotMapped]
        public decimal FinalBalance => InitialBalance + CurrentBalance;
    }
}    
