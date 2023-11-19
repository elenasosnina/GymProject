namespace GymProject.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Lesson")]
    public partial class LessonEntity
    {
        public long ID { get; set; }

        [Column("date and time")]
        public long DateAndTime { get; set; }

        [Column("ID trainer")]
        public long TrainerId { get; set; }

        [Column("ID hall")]
        public long HallId { get; set; }

        [Column("ID subscription")]
        public long SubscriptionId { get; set; }

        [Column("ID program")]
        public long ProgramId { get; set; }

        [Column("ID gym")]
        public long GymId { get; set; }

        public virtual GymEntity Gym { get; set; }

        public virtual HallEntity Hall { get; set; }

        public virtual PositionEntity Position { get; set; }

        public virtual SubscriptionEntity Subscription { get; set; }

        public virtual LessonProgramEntity Lesson_programs { get; set; }
    }
}
