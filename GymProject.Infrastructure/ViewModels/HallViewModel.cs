using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.ViewModels
{
    public partial class HallViewModel// Класс представления данных тренажерного зала.
    {
        public long Id { get; set; }
        public decimal HallNumber { get; set; }
    }
}
