namespace BakeryShop.Models
{
    public class CartItemVewModel
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public byte[]? ProductImage { get; set; }
        public Double Price { get; set; }
        public int Quantity { get; set; }
        public Double TotalPrice => Price * Quantity;
    }
}
