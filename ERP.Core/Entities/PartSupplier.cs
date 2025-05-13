namespace ERP.Core.Entities
{
    public class PartSupplier
    {
        public int PartSupplierID { get; set; }
        public required string SupplierName { get; set; }
        public string? PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public virtual PartSupplierBalance PartSupplierBalance { get; set; }
        public virtual ICollection<AssemblyPart> Parts { get; set; } = new List<AssemblyPart>();
        public virtual ICollection<PartSupplierOrder> PartSuppliersOrders { get; set; } = new List<PartSupplierOrder>();
        public virtual ICollection<PartSupplierTransaction> PartSupplierTransactions { get; set; } = new List<PartSupplierTransaction>();
    }
}
