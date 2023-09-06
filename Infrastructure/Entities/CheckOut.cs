using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    [Table("CheckoutOrders")]
    public class CheckOut
    {
        [Key]
        public int? IdOrder { get; set; }
        public int? CustomerId { get; set; }
        public string? Note { get; set; }
        public bool? IsReceived { get; set; }
    }
}
