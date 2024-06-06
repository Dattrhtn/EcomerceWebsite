namespace EcomerceWebsite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Order()
        {
            order_item = new HashSet<order_item>();
        }

        [Key]
        public int order_id { get; set; }

        public decimal? total_price { get; set; }

        public int? account_account_id { get; set; }

        public int? Payment_payment_id { get; set; }

        public int? Shipment_shipment_id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ngayTao { get; set; }

        public virtual account account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_item> order_item { get; set; }

        public virtual Payment Payment { get; set; }

        public virtual shipment shipment { get; set; }
    }
}
