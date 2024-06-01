using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EcomerceWebsite.Models
{
    public partial class Wishlist
    {
        public int WishlistId { get; set; }
        public int CustomerCustomerId { get; set; }
        public int? ProductProductId { get; set; }

        public virtual Account CustomerCustomer { get; set; }
        public virtual Product ProductProduct { get; set; }
    }
}
