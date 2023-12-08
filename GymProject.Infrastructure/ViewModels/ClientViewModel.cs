using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.ViewModels
{
    public partial class ClientViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string SecondName { get; set; }
        public string MiddleName { get; set; }
        public string DateOfBirth { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public long DiscountId { get; set; }
        public DiscountViewModel Discount { get; set; }
    }

}
