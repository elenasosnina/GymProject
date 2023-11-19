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
        public string name { get; set; }

        public decimal cost { get; set; }

        public decimal duration { get; set; }

        [Column("number of classes")]
        public decimal number_of_classes { get; set; }

        [Column("date and time of purchase")]
        public decimal date_and_time_of_purchase { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<SubscriptionEntity> Subscription { get; set; }
    }
}
