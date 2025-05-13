using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ERP.Core.Entities
{
    public class SupplierOrder
    {
        [Key]
        public int SupplierOrderID { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; } 
        //public CustomerOrderSource OrderType { get; set; } 
        public SupplierOrderType OrderType { get; set; }

        public int SupplierID { get; set; }
        public virtual Supplier Supplier { get; set; }

        public virtual ICollection<SupplierOrderDetail> OrderDetails { get; set; }
    }

    public enum SupplierOrderType
    {
        [EnumMember(Value = "مشتريات")]
        Purchase,
        [EnumMember(Value = "مرتجع")]
        Refund
    }

}
