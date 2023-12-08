using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.ViewModels
{
    public partial class ProductViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Cost { get; set; }
        public decimal Quantity { get; set; }
        public string ExpirationDate { get; set; }
        public long ProductCategoryId { get; set; }
        public ProductCategoryViewModel ProductCategory { get; set; }
    }
}
