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

        [Column("ID")]
        public long Id { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Name { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string SecondName { get; set; }

        [Column("middle name")]
        [StringLength(2147483647)]
        public string MiddleName { get; set; }

        [Column("date of birth")]
        public string DateOfBirth { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Login { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Password { get; set; }

        [Column("ID discount")]
        public long DiscountId { get; set; }

        public virtual DiscountEntity Discount { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubscriptionEntity> Subscription { get; set; }
    }
}
