using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    [Table("OrderDetails")]
    public class OrderDetail
    {
        [Key]
        public int? OrderDetailID { get; set; }
        public int? ProductID { get; set; }
        public int? OrderID { get; set; }
        public int? Quantity { get; set; }
        public Double? Subtotal { get; set; }
        public Double? DiscountMoney { get; set; }

    }
}
