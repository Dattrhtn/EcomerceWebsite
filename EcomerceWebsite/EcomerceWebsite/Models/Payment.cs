using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace EcomerceWebsite.Models
{
    public partial class Payment
    {
        public Payment()
        {
            Order = new HashSet<Order>();
        }

        public int PaymentId { get; set; }
        public DateTime? PaymentDate { get; set; }
        public string PaymentMethod { get; set; }
        public decimal? Amount { get; set; }
        public int? CustomerCustomerId { get; set; }

        public virtual Account CustomerCustomer { get; set; }
        public virtual ICollection<Order> Order { get; set; }
    }
}
