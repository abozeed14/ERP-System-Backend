﻿
using System.Runtime.Serialization;

namespace ERP.Core.Entities
{
    public class Customer
    {
        public int CustomerID { get; set; }
        public required string CustomerName { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; } = true;
        // public bool IsDoupleAgent { get; set; } = false ;
        public CustomerType CustomerType { get; set; } = CustomerType.Regular;

        // Navigation Properties
        public virtual CustomerBalance CustomerBalance { get; set; }
        public virtual ICollection<CustomerOrder> CustomerOrders { get; set; } = new List<CustomerOrder>();
        public virtual ICollection<CustomerOrderTransaction> CustomerOrderTransactions { get; set; } = new List<CustomerOrderTransaction>();
    }

    public enum CustomerType
    {
        [EnumMember(Value = "جديد")]
        New,
        [EnumMember(Value = "دائم")]
        Regular,
        [EnumMember(Value = "تجاري")]
        Commercial
    }
}
