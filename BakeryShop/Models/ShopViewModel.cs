using X.PagedList;

namespace BakeryShop.Models
{
    public class ShopViewModel
    {
        public IPagedList<ProductViewModel>? PagedProducts { get; set; }
        public List<CategoryViewModel>? Categories { get; set; }
    }
}
