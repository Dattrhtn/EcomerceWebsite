namespace EcomerceWebsite.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("account")]
    public partial class account
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public account()
        {
            carts = new HashSet<cart>();
            Orders = new HashSet<Order>();
            shipments = new HashSet<shipment>();
            wishlists = new HashSet<wishlist>();
        }

        [Key]
        public int account_id { get; set; }

        [StringLength(100)]
        [Display(Name = "Tên")]
        public string first_name { get; set; }

        [StringLength(100)]
        [Display(Name = "Họ")]
        public string last_name { get; set; }

        [StringLength(100)]
        [Display(Name = "Email")]
        public string email { get; set; }

        [StringLength(100)]
        [Display(Name = "Mật khẩu")]
        public string password { get; set; }

        [StringLength(100)]
        [Display(Name = "Địa chỉ")]
        public string address { get; set; }

        [Display(Name = "Quyền")]
        public int? role { get; set; }

        [StringLength(100)]
        [Display(Name = "Số điện thoại")]
        public string phone_number { get; set; }

        [Column(TypeName = "datetime2")]
        [Display(Name = "Ngày tạo")]
        public DateTime ngayTao { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<cart> carts { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Order> Orders { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<shipment> shipments { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<wishlist> wishlists { get; set; }
    }
}
