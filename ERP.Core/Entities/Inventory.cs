using System.ComponentModel.DataAnnotations;

namespace ERP.Core.Entities
{
    public class Inventory
    { 

        [Key]
        public int InventoryID { get; set; }
        public required string Name { get; set; }
        public string? Address { get; set; }
        public string? ContactNumber { get; set; }
        public InventoryType InventoryType {  get; set; }
        public bool IsActive { get; set; } = true;
    }
    
    public enum InventoryType
    {
        Warehouse,
        Gallery
    }
}
