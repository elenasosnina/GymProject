using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.ViewModels
{
    public partial class PositionViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public decimal Salary { get; set; }
        public string WorkSchedule { get; set; }
    }
}
