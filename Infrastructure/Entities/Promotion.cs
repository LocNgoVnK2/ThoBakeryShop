using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    [Table("Promotions")]
    public class Promotion
    {
        [Key]
        public int? PromotionID { get; set; }
        public string? PromotionName { get; set; }
        public string? Description { get; set; }
        public string? Discount { get; set; }
    }
}
