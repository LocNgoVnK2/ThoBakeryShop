using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entities
{
    [Table("Reviews")]
    public class Review
    {
        [Key]
        public int? ReviewID { get; set; }
        public int? ProductID { get; set; }
        public int? CustomerID { get; set; }
        public int? Rating { get; set; }
   
        public string? Comment { get; set; }
    }
}
