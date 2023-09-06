namespace BakeryShop.Models
{
    public class OrderDetailViewModel
    {
        public int? OrderDetailID { get; set; }
        public int? ProductID { get; set; }

        public string ProductName { get; set; }
        public int? OrderID { get; set; }
        public int? Quantity { get; set; }
        public Double? Subtotal { get; set; }
        public Double? DiscountMoney { get; set; }
    }
}
