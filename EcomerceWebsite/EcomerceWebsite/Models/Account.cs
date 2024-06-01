using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EcomerceWebsite.Models
{
    public partial class Account
    {
        public Account()
        {
            Cart = new HashSet<Cart>();
            Order = new HashSet<Order>();
            Payment = new HashSet<Payment>();
            Shipment = new HashSet<Shipment>();
            Wishlist = new HashSet<Wishlist>();
        }

        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public int? Role { get; set; }
        public string Username { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<Order> Order { get; set; }
        public virtual ICollection<Payment> Payment { get; set; }
        public virtual ICollection<Shipment> Shipment { get; set; }
        public virtual ICollection<Wishlist> Wishlist { get; set; }
    }
}
