using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class EmployeeRepository
    {
        public List<EmployeeEntity> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Employees.ToList();
                return items;
            }
        }
        public EmployeeEntity GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Employees.FirstOrDefault(x => x.Id == id);
                return item;
            }
        }

    }
}
