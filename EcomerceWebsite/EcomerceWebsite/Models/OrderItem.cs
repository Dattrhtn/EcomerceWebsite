using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EcomerceWebsite.Models
{
    public partial class OrderItem
    {
        public int OrderItemId { get; set; }
        public int? Quantity { get; set; }
        public decimal? Price { get; set; }
        public int? ProductProductId { get; set; }
        public int OrderOrderId { get; set; }

        public virtual Order OrderOrder { get; set; }
        public virtual Product ProductProduct { get; set; }
    }
}
