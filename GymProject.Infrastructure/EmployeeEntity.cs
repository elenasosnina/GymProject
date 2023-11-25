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
        public string Name { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Surname { get; set; }

        [Column("middle name")]
        [StringLength(2147483647)]
        public string MiddleName { get; set; }

        [Column("date of birth")]
        public decimal DateOfBirth { get; set; }

        public decimal Gender { get; set; }

        [Column("length of service")]
        public decimal LengthOfService { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Login { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Password { get; set; }

        [Column("ID position")]
        public long PositionId { get; set; }

        public virtual PositionEntity Position { get; set; }
    }
}
