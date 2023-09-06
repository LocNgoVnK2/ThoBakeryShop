using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    [Table("Orders")]
    public class Order
    {
        [Key]
        public int? OrderID { get; set; }
        public DateTime? OrderDate { get; set; }
        public Double? TotalAmount { get; set; }

        public int? AccountId { get; set; } // khi account này khác null có nghĩa là đơn hàng đã dc xác nhận 
        public bool? IsDone { get; set; }
    }
}
