namespace GymProject.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Client")]
    public partial class ClientEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ClientEntity()
        {
            Subscription = new HashSet<SubscriptionEntity>();
        }

        public long ID { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string name { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string secondname { get; set; }

        [Column("middle name")]
        [StringLength(2147483647)]
        public string middle_name { get; set; }

        [Column("date of birth")]
        public decimal date_of_birth { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string login { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string password { get; set; }

        [Column("ID discount")]
        [Required]
        [StringLength(2147483647)]
        public string ID_discount { get; set; }

        public virtual DiscountEntity Discount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubscriptionEntity> Subscription { get; set; }
    }
}
