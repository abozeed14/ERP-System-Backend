﻿namespace ERP.Core.Entities
{
    public class SupplierOrderDetail
    {
        public int SupplierOrderDetailID { get; set; }
        public int SupplierOrderID { get; set; }
        public virtual SupplierOrder SupplierOrder { get; set; }
        public int ProductID { get; set; }
        public virtual Product Product { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; } // Price per unit at the time of billing
        public decimal ProductTotalPrice => Quantity * UnitPrice ;
    }

}
