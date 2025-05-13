namespace ERP.Core.Entities
{
    public class PartSupplierOrderDetail
    {
        public int PartSupplierOrderDetailID { get; set; }
        public int PartSupplierOrderID { get; set; }
        public virtual PartSupplierOrder PartSupplierOrder { get; set; }
        public int PartID { get; set; }
        public virtual AssemblyPart Part { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } // Price per unit at the time of billing
        public decimal PartTotalPrice => Quantity * UnitPrice;
    }
}
