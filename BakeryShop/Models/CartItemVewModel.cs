﻿namespace BakeryShop.Models
{
    public class CartItemVewModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public byte[]? ProductImage { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public decimal? TotalPrice => Price * Quantity;
    }
}
