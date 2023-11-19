namespace GymProject.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Employee")]
    public partial class EmployeeEntity
    {
        public long ID { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string name { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string surname { get; set; }

        [Column("middle name")]
        [StringLength(2147483647)]
        public string middle_name { get; set; }

        [Column("date of birth")]
        public decimal date_of_birth { get; set; }

        public decimal gender { get; set; }

        [Column("length of service")]
        public decimal length_of_service { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string login { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string password { get; set; }

        [Column("ID position")]
        public long ID_position { get; set; }

        public virtual PositionEntity Position { get; set; }
    }
}
