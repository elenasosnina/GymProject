namespace GymProject.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Subscription type")]
    public partial class SubscriptionTypeEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubscriptionTypeEntity()
        {
            Subscription = new HashSet<SubscriptionEntity>();
        }

        public long ID { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Name { get; set; }

        public decimal Cost { get; set; }

        public decimal Duration { get; set; }

        [Column("number of classes")]
        public decimal NumberOfClasses { get; set; }

        [Column("date and time of purchase")]
        public decimal DateAndTimeOfPurchase { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubscriptionEntity> Subscription { get; set; }
    }
}
