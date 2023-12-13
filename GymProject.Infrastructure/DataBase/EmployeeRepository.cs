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
        public EmployeeViewModel Update(EmployeeEntity entity)// Метод для обновления данных сотрудника в базе данных.
        {// Обрезка строковых полей от лишних пробелов.
            entity.Name = entity.Name.Trim();
            entity.SurName = entity.SurName.Trim();
            entity.MiddleName = entity.MiddleName.Trim();
            entity.DateOfBirth = entity.DateOfBirth.Trim();
            entity.Gender = entity.Gender.Trim();
            entity.LengthOfService = entity.LengthOfService;
            entity.Login = entity.Login.Trim();
            entity.Password = entity.Password.Trim();
            // Проверка наличия заполненных полей.
            if (string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.SurName) || string.IsNullOrEmpty(entity.MiddleName) || string.IsNullOrEmpty(entity.Gender) || string.IsNullOrEmpty(entity.LengthOfService.ToString()) || string.IsNullOrEmpty(entity.Login) || string.IsNullOrEmpty(entity.Password))
            {
                throw new Exception("Не все поля заполнены");
            }
            using (var context = new Context())
            {
                var existingClient = context.Employees.Find(entity.Id);

                if (existingClient != null)
                {// Обновление данных существующего сотрудника.
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return EmployeeMapper.Map(entity);
        }
        public EmployeeViewModel Delete(long id)// Метод для удаления сотрудника из базы данных по идентификатору.
        {
            using (var context = new Context())
            {
                var clientToRemove = context.Employees.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.Employees.Remove(clientToRemove);// Удаление сотрудника из базы данных.
                    context.SaveChanges();
                }
                return EmployeeMapper.Map(clientToRemove);
            }
        }
        public EmployeeViewModel Add(EmployeeEntity entity)// Метод для добавления нового сотрудника в базу данных.
        {// Обрезка строковых полей от лишних пробелов.
            entity.Name = entity.Name.Trim();
            entity.SurName = entity.SurName.Trim();
            entity.MiddleName = entity.MiddleName.Trim();
            entity.DateOfBirth = entity.DateOfBirth.Trim();
            entity.Gender = entity.Gender.Trim();
            entity.LengthOfService = entity.LengthOfService;
            entity.Login = entity.Login.Trim();
            entity.Password = entity.Password.Trim();
            // Проверка наличия заполненных полей.
            if (string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.SurName) || string.IsNullOrEmpty(entity.MiddleName) || string.IsNullOrEmpty(entity.Gender) || string.IsNullOrEmpty(entity.LengthOfService.ToString()) || string.IsNullOrEmpty(entity.Login) || string.IsNullOrEmpty(entity.Password))
            {
                throw new Exception("Не все поля заполнены");
            }
            using (var context = new Context())
            {
                context.Employees.Add(entity);// Добавление нового сотрудника в базу данных.
                context.SaveChanges();
            }
            return EmployeeMapper.Map(entity);
        }
        public List<EmployeeViewModel> GetList()// Метод для получения списка сотрудников из базы данных.
        {
            using (var context = new Context())
            {
                var items = context.Employees.Include(x => x.Position).ToList();// Извлечение сотрудников из базы данных, включая связанные сущности, такие как должность.
                return EmployeeMapper.Map(items);// Преобразование сущностей в ViewModel.
            }
        }
        public EmployeeViewModel GetById(long id)// Метод для получения сотрудника по идентификатору из базы данных.
        {
            using (var context = new Context())
            {
                var item = context.Employees.FirstOrDefault(x => x.Id == id);
                return EmployeeMapper.Map(item);// Преобразование сущности в ViewModel.
            }
        }
        public List<EmployeeViewModel> Search(string search)// Метод для поиска сотрудников по имени в базе данных.
        {
            search = search.Trim().ToLower(); // Обрезка строки поиска и приведение к нижнему регистру.

            using (var context = new Context())
            {// Поиск сотрудников по имени в базе данных, включая связанные сущности, такие как должность.
                var result = context.Employees.Include(x => x.Position).Where(x => x.Name.ToLower().Contains(search) && x.Name.Length == search.Length).ToList();
                return EmployeeMapper.Map(result);
            }

        }
        public EmployeeViewModel Login(string login, string password)// Метод для аутентификации сотрудника по логину и паролю.
        {
            using (var context = new Context())
            {// Поиск сотрудника по логину и паролю в базе данных.
                var item = context.Employees.Include(x => x.Position).FirstOrDefault(x => x.Login == login && x.Password == password);

                return EmployeeMapper.Map(item);
            }
        }
    }
}
