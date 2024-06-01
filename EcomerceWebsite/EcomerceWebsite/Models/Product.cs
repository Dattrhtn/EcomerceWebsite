using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EcomerceWebsite.Models
{
    public partial class Product
    {
        public Product()
        {
            Cart = new HashSet<Cart>();
            OrderItem = new HashSet<OrderItem>();
            Wishlist = new HashSet<Wishlist>();
        }

        public int ProductId { get; set; }
        public string Sku { get; set; }
        public string Description { get; set; }
        public decimal? Price { get; set; }
        public int? Stock { get; set; }
        public int? CategoryCategoryId { get; set; }
        public string Image { get; set; }
        public DateTime? Ngaytao { get; set; }

        public virtual Category CategoryCategory { get; set; }
        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
        public virtual ICollection<Wishlist> Wishlist { get; set; }
    }
}
