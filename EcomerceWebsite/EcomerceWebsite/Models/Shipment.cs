namespace EcomerceWebsite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("shipment")]
    public partial class shipment
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public shipment()
        {
            Orders = new HashSet<Order>();
        }

        [Key]
        public int shipment_id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime? shipment_date { get; set; }

        [StringLength(255)]
        public string address { get; set; }

        [StringLength(100)]
        public string city { get; set; }

        [StringLength(255)]
        public string state { get; set; }

        [StringLength(50)]
        public string country { get; set; }

        [StringLength(10)]
        public string zip_code { get; set; }

        public int? account_account_id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ngayTao { get; set; }

        public virtual account account { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }
    }
}
