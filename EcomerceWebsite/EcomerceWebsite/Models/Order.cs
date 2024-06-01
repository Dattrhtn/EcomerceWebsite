using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EcomerceWebsite.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItem = new HashSet<OrderItem>();
        }

        public int OrderId { get; set; }
        public DateTime? OrderDate { get; set; }
        public decimal? TotalPrice { get; set; }
        public int? CustomerCustomerId { get; set; }
        public int? PaymentPaymentId { get; set; }
        public int? ShipmentShipmentId { get; set; }

        public virtual Account CustomerCustomer { get; set; }
        public virtual Payment PaymentPayment { get; set; }
        public virtual Shipment ShipmentShipment { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
