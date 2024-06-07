namespace EcomerceWebsite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Product()
        {
            carts = new HashSet<cart>();
            order_item = new HashSet<order_item>();
            wishlists = new HashSet<wishlist>();
        }

        [Key]
        public int product_id { get; set; }

        [StringLength(100)]
        public string productCode { get; set; }

        [StringLength(100)]
        public string SKU { get; set; }

        public string description { get; set; }

        public decimal? price { get; set; }

        [StringLength(200)]
        public string name { get; set; }

        [StringLength(10)]
        public string size { get; set; }

        public int? color { get; set; }

        public int? quantity { get; set; }

        public int? stock { get; set; }

        public int? Category_category_id { get; set; }

        [StringLength(500)]
        public string Image { get; set; }

        [Column(TypeName = "datetime2")]
        public DateTime ngayTao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart> carts { get; set; }

        public virtual Category Category { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<order_item> order_item { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<wishlist> wishlists { get; set; }
    }
}
