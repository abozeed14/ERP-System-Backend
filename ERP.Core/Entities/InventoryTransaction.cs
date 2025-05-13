using System.Runtime.Serialization;

namespace ERP.Core.Entities
{
    public class InventoryTransaction
    {
        public int ID { get; set; }
        public string? Description { get; set; }
        public InventoryTransactionType InventoryTransactionType { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public List<InventoryTransactionDetail> InventoryTransactionDetails { get; set; } = [];
    }
    public enum InventoryTransactionType
    {
        [EnumMember(Value = "اذن صرف")]
        FromInventory,
        [EnumMember(Value = "اذن توريد")]
        ToInventory,
    }
}
