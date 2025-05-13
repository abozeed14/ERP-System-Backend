using System.ComponentModel.DataAnnotations;

namespace ERP.Core.Entities
{
    public class Supplier
    {
        [Key]
        public int SupplierID { get; set; }
        public required string SupplierName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
    //    public bool IsDoupleAgent { get; set; } = false;
        public virtual SupplierBalance SupplierBalance { get; set; }
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public virtual ICollection<SupplierOrder> SuppliersOrders { get; set; } = new List<SupplierOrder>();
        public virtual ICollection<SupplierOrderTransaction> SupplierOrderTransactions { get; set; } = new List<SupplierOrderTransaction>();

    }
}
