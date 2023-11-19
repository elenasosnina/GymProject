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
        public string name { get; set; }

        public decimal cost { get; set; }

        public decimal quantity { get; set; }

        [Column("expiration date")]
        public decimal expiration_date { get; set; }

        [Column("ID product category")]
        public long ID_product_category { get; set; }

        public virtual ProductCategoryEntity Product_category { get; set; }
    }
}
