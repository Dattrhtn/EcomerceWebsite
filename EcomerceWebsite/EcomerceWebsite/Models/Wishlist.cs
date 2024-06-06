namespace EcomerceWebsite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("wishlist")]
    public partial class wishlist
    {
        [Key]
        [Column(Order = 0)]
        public int wishlist_id { get; set; }

        [Key]
        [Column(Order = 1)]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int account_account_id { get; set; }

        public int? product_product_id { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ngayTao { get; set; }

        public virtual account account { get; set; }

        public virtual Product Product { get; set; }
    }
}
