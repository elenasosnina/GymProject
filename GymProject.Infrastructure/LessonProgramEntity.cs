namespace GymProject.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Lesson programs")]
    public partial class LessonProgramEntity
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LessonProgramEntity()
        {
            Lesson = new HashSet<LessonEntity>();
        }

        public long ID { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string name { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string description { get; set; }

        [Column("program duration")]
        public decimal program_duration { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LessonEntity> Lesson { get; set; }
    }
}
