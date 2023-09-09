

namespace BakeryShop.Models
{
    public class CheckOutViewModel
    {
        public int? IdOrder { get; set; }
        public int? CustomerId { get; set; }
        public string? Note { get; set; }
        public bool? IsReceived { get; set; }
        public bool? IsAccept { get; set; }
       

        public DateTime? OrderDate { get; set; }
        public Double? TotalPrice { get; set; }
        public Double? DiscountPrice { get; set; }
        public string? FirstName { get; set;}

        public string? LastName { get; set;}
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
       


    }
}
