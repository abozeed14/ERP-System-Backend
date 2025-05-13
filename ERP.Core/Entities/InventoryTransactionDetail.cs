namespace ERP.Core.Entities
{
    public class InventoryTransactionDetail
    {
        public int ID { get; set; }
        public int ProductID { get; set; }
        public int Quantity { get; set; }
        public int InventoryTransactionID { get; set; }
        //Navigation Properties
        public virtual Product Product { get; set; }
        public virtual InventoryTransaction InventoryTransaction { get; set; }
    }
}
