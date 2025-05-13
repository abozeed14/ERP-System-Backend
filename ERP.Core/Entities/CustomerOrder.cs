using System.Runtime.Serialization;


namespace ERP.Core.Entities
{
    public class CustomerOrder
    {
        public int CustomerOrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public CustomerOrderSource OrderSource { get; set; }
        public CustomerOrderStatues OrderStatues { get; set; }
        public CustomerOrderType OrderType { get; set; }
        public CustomerPaymentType PaymentType { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal Discount { get; set; }
        public decimal Profit { get; set; }
        public int CustomerID { get; set; }
        public virtual Customer Customer { get; set; }

        public virtual ICollection<CustomerOrderDetail> OrderDetails { get; set; } =new List<CustomerOrderDetail>();


        //public decimal TotalPrice => OrderDetails?.Sum(detail => detail.ProductTotalPrice) ?? 0;
    }

    public enum CustomerOrderSource
    {
        [EnumMember(Value = "معرض")]
        Galary,

        [EnumMember(Value = "عبر الإنترنت")]
        Online
    }

    public enum CustomerOrderStatues
    {
        [EnumMember(Value = "قيد الانتظار")]
        Pending,

        [EnumMember(Value = "تم الشحن")]
        Shipped,

        [EnumMember(Value = "مكتمل")]
        Completed
    }

    public enum CustomerPaymentType
    {
        [EnumMember(Value = "كاش")]
        Cash,

        [EnumMember(Value = "اجل")]
        Depit
    }

    public enum CustomerOrderType
    {
        [EnumMember(Value = "مبيعات")]
        Selling,

        [EnumMember(Value = "مرتجع")]
        Refund
    }


}
