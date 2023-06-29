namespace BakeryShop.Models
{
    public class CategoryViewModel
    {
        public int? CategoryID { get; set; }
        public string CategoryName { get; set; }
        public bool? IsUsed { get; set; }= true;
    }
}
