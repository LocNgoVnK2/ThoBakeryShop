using Infrastructure.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BakeryShop.Models
{
        public class ProductViewModel
        {
            public int? ProductID { get; set; }

            [Required]
            [MaxLength(255)]
            public string? ProductName { get; set; }

            [MaxLength]
            public string? Description { get; set; }

            [Required]
 
            public Double? Price { get; set; }

            public IFormFile? ImageData { get; set; }

            public byte[]? Image { get; set; }
            [ForeignKey("Category")]
            public int? CategoryId { get; set; }
            public Category? Category { get; set; }
            public bool? IsUsed { get; set; } = true;
            public IEnumerable<CategoryViewModel>? Categories { get; set; }
            public List<Review> reviews { get; set; }
            public int? Rating { get; set; }
        }
}
