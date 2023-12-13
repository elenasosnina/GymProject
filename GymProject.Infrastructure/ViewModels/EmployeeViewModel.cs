using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.ViewModels
{
    public partial class EmployeeViewModel// Класс представления данных сотрудника.
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string SurName { get; set; }
        public string MiddleName { get; set; }
        public string DateOfBirth { get; set; }
        public string Gender { get; set; }
        public decimal LengthOfService { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public long PositionId { get; set; }
        public PositionViewModel Position { get; set; }
    }
}
