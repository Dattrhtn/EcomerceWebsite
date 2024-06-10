namespace EcomerceWebsite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class order_item
    {
        [Key]
        public int order_item_id { get; set; }

        public int? quantity { get; set; }

        public decimal? price { get; set; }

        public int? product_product_id { get; set; }

        public int order_order_id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ngayTao { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}
