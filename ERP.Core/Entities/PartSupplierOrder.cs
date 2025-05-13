using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace ERP.Core.Entities
{
    public class PartSupplierOrder
    {
        [Key]
        public int PartSupplierOrderID { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime OrderDate { get; set; }
        public PartSupplierOrderType OrderType { get; set; }

        public int PartSupplierID { get; set; }
        public virtual PartSupplier PartSupplier { get; set; }

        public virtual ICollection<PartSupplierOrderDetail> OrderDetails { get; set; }
    }

    public enum PartSupplierOrderType
    {
        [EnumMember(Value = "مشتريات")]
        Purchase,
        [EnumMember(Value = "مرتجع")]
        Refund
    }
}
