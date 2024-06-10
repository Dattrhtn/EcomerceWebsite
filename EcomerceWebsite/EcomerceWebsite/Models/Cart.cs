namespace EcomerceWebsite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("cart")]
    public partial class cart
    {
        [Key]
        public int cart_id { get; set; }

        public int? quantity { get; set; }

        public int account_account_id { get; set; }

        public int? product_product_id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ngayTao { get; set; }

        public virtual account account { get; set; }

        public virtual Product Product { get; set; }
    }
}
