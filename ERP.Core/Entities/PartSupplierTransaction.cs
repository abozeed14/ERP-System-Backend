using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ERP.Core.Entities
{
    public class PartSupplierTransaction
    {
        [Key]
        public int TransactionID { get; set; }
        public string Description { get; set; } = string.Empty;
        public PartSupplierTransactionType TransactionType { get; set; }
        public decimal Amount { get; set; } // Amount of money in the transaction 
        public DateTime TransactionDate { get; set; }

        public int PartSupplierID { get; set; }
        public virtual PartSupplier PartSupplier { get; set; }
    }

    public enum PartSupplierTransactionType
    {
        [EnumMember(Value = "دفع")]
        Payment,
        [EnumMember(Value = "خصم")]
        Discount,
    }
}
