using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.ViewModels
{
    public partial class DiscountViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public decimal Value { get; set; }
       
    }
}
