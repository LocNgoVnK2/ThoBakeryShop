using Infrastructure.Entities;

namespace BakeryShop.Models
{
    public class OrderViewModel
    {
        public int? OrderID { get; set; }
        public int? OrderDate { get; set; }
        public int? TotalAmount { get; set; }

        public int? AccountId { get; set; } // khi account này khác null có nghĩa là đơn hàng đã dc xác nhận 
        public bool? IsDone { get; set; }
        public List<OrderDetail>? orderDetails { get; set; }
    }
}
