namespace GymProject.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Subscription")]
    public partial class SubscriptionEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public SubscriptionEntity()
        {
            Lesson = new HashSet<LessonEntity>();
        }

        public long ID { get; set; }

        [Column("validity start date")]
        public decimal validity_start_date { get; set; }

        [Column("validity expiration date")]
        public decimal validity_expiration_date { get; set; }

        [Column("ID trainer")]
        public long ID_trainer { get; set; }

        [Column("ID client")]
        public long ID_client { get; set; }

        [Column("ID status")]
        public long ID_status { get; set; }

        [Column("ID subscription type")]
        public long ID_subscription_type { get; set; }

        public virtual ClientEntity Client { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonEntity> Lesson { get; set; }

        public virtual PositionEntity Position { get; set; }

        public virtual StatusEntity Status { get; set; }

        public virtual SubscriptionTypeEntity Subscription_type { get; set; }
    }
}
