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
            entity.SurName = entity.SurName.Trim();
            entity.MiddleName = entity.MiddleName.Trim();
            entity.DateOfBirth = entity.DateOfBirth.Trim();
            entity.Gender = entity.Gender.Trim();
            entity.LengthOfService = entity.LengthOfService;
            entity.Login = entity.Login.Trim();
            entity.Password = entity.Password.Trim();

            if (string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.SurName) || string.IsNullOrEmpty(entity.MiddleName) || string.IsNullOrEmpty(entity.Gender) || string.IsNullOrEmpty(entity.LengthOfService.ToString()) || string.IsNullOrEmpty(entity.Login) || string.IsNullOrEmpty(entity.Password))
            {
                throw new Exception("Не все поля заполнены");
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
            entity.Name = entity.Name.Trim();
            entity.SurName = entity.SurName.Trim();
            entity.MiddleName = entity.MiddleName.Trim();
            entity.DateOfBirth = entity.DateOfBirth.Trim();
            entity.Gender = entity.Gender.Trim();
            entity.LengthOfService = entity.LengthOfService;
            entity.Login = entity.Login.Trim();
            entity.Password = entity.Password.Trim();

            if (string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.SurName) || string.IsNullOrEmpty(entity.MiddleName) || string.IsNullOrEmpty(entity.Gender) || string.IsNullOrEmpty(entity.LengthOfService.ToString()) || string.IsNullOrEmpty(entity.Login) || string.IsNullOrEmpty(entity.Password))
            {
                throw new Exception("Не все поля заполнены");
            }
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
        public List<EmployeeViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.Employees.Include(x => x.Position).Where(x => x.Name.ToLower().Contains(search) && x.Name.Length == search.Length).ToList();
                return EmployeeMapper.Map(result);
            }

        }
        public EmployeeViewModel Login(string login, string password)
        {
            using (var context = new Context())
            {
                var item = context.Employees.Include(x => x.Position).FirstOrDefault(x => x.Login == login && x.Password == password);

                return EmployeeMapper.Map(item);
            }
        }
    }
}
