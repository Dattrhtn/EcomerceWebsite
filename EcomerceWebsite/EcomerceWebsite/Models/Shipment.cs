using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EcomerceWebsite.Models
{
    public partial class Shipment
    {
        public Shipment()
        {
            Order = new HashSet<Order>();
        }

        public int ShipmentId { get; set; }
        public DateTime? ShipmentDate { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string ZipCode { get; set; }
        public int? CustomerCustomerId { get; set; }

        public virtual Account CustomerCustomer { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
