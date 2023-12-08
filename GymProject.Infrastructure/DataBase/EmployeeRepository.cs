using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Text.RegularExpressions;

namespace GymProject.Infrastructure.DataBase
{
    public class EmployeeRepository
    {
        public EmployeeViewModel Update(EmployeeEntity entity)
        {
            entity.Name = entity.Name.Trim();
            if (string.IsNullOrEmpty(entity.Name))
            {
                throw new Exception("Имя пользователя не может быть пустым");
            }
            using (var context = new Context())
            {
                var existingClient = context.Employees.Find(entity.Id);

                if (existingClient != null)
                {
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return EmployeeMapper.Map(entity);
        }
        public EmployeeViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var clientToRemove = context.Employees.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.Employees.Remove(clientToRemove);
                    context.SaveChanges();
                }
                return EmployeeMapper.Map(clientToRemove);
            }
        }
        public EmployeeViewModel Add(EmployeeEntity entity)
        {
            using (var context = new Context())
            {
                context.Employees.Add(entity);
                context.SaveChanges();
            }
            return EmployeeMapper.Map(entity);
        }
        public List<EmployeeViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Employees.Include(x => x.Position).ToList();
                return EmployeeMapper.Map(items);
            }
        }
        public EmployeeViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Employees.FirstOrDefault(x => x.Id == id);
                return EmployeeMapper.Map(item);
            }
        }
    }
}
