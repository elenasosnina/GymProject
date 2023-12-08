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
        [Column("ID")]
        public long Id { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Name { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string SurName { get; set; }

        [Column("middle name")]
        [StringLength(2147483647)]
        public string MiddleName { get; set; }

        [Column("date of birth")]
        public string DateOfBirth { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Gender { get; set; }

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
