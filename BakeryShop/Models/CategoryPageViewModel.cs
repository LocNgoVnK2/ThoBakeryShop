namespace BakeryShop.Models
{
    public class CategoryPageViewModel
    {
        public IEnumerable<CategoryViewModel> Categories { get; set; }
        public CategoryViewModel NewCategory { get; set; }
    }
}
