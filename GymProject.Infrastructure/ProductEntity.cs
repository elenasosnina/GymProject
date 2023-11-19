namespace GymProject.Infrastructure
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class ProductEntity
    {
        public long ID { get; set; }

        [Required]
        [StringLength(2147483647)]
        public string Name { get; set; }

        public decimal Cost { get; set; }

        public decimal Quantity { get; set; }

        [Column("expiration date")]
        public decimal ExpirationDate { get; set; }

        [Column("ID product category")]
        public long ProductCategoryId { get; set; }

        public virtual ProductCategoryEntity Product_category { get; set; }
    }
}
