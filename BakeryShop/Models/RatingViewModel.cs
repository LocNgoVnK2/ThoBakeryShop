namespace BakeryShop.Models
{
    public class RatingViewModel
    {
        public int? ReviewID { get; set; }
        public int? ProductID { get; set; }
        public string? ProductName { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Rating { get; set; }

        public string? Comment { get; set; }
    }
}
