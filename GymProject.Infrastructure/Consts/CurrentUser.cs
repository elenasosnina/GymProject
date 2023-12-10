using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.Consts
{
    public class CurrentUser
    {
        public static string PositionId { get; set; }
        public static string PositionName { get; set; }
        public static string EmployeeName { get; set; }
        public static string EmployeeId { get; set; }
       
        Dictionary<string, string> currentUser = new Dictionary<string, string>()
        {
            { "user", "Администратор" },
            { "user2", "Пользователь" },
            { "user3", "Пользователь" }
        };

    }
}
