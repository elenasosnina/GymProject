using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.Consts
{
    public class CurrentUser
    { // Свойства для идентификации текущего пользователя.
        public static string PositionId { get; set; }
        public static string PositionName { get; set; }
        public static string EmployeeName { get; set; }
        public static string EmployeeId { get; set; }
        // Словарь, содержащий информацию о пользователях приложения.
        Dictionary<string, string> currentUser = new Dictionary<string, string>()
        {
            { "user", "Администратор" },
            { "user2", "Пользователь" },
            { "user3", "Пользователь" }
        };

    }
}
