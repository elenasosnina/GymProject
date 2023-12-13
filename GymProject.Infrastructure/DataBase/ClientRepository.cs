using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class ClientRepository
    {

        public List<ClientViewModel> GetList()// Метод для получения списка клиентов из базы данных.
        {
            using (var context = new Context())
            {
                var items = context.Clients.Include(x => x.Discount).ToList();// Извлечение клиентов из базы данных, включая связанные сущности, такие как скидки.
                return ClientMapper.Map(items);// Преобразование сущностей в ViewModel.
            }
        }
        public ClientViewModel GetById(long id)// Метод для получения клиента по идентификатору из базы данных.
        {
            using (var context = new Context())
            {
                var item = context.Clients.FirstOrDefault(x => x.Id == id);

                return ClientMapper.Map(item);// Преобразование сущности в ViewModel.
            }

        }
        public ClientViewModel Update(ClientEntity entity) // Метод для обновления данных клиента в базе данных.
        {// Обрезка строковых полей от лишних пробелов.
            entity.Name = entity.Name.Trim();
            entity.SecondName = entity.SecondName.Trim();
            entity.MiddleName = entity.MiddleName.Trim();
            entity.DateOfBirth = entity.DateOfBirth.Trim();
            entity.Login = entity.Login.Trim();
            entity.Password = entity.Password.Trim();
            // Проверка наличия заполненных полей.
            if (string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.SecondName) || string.IsNullOrEmpty(entity.MiddleName) || string.IsNullOrEmpty(entity.DateOfBirth) || string.IsNullOrEmpty(entity.Login) || string.IsNullOrEmpty(entity.Password))
            {
                throw new Exception("Не все поля заполнены");
            }

            using (var context = new Context())
            {
                var existingClient = context.Clients.Find(entity.Id);

                if (existingClient != null)
                {// Обновление данных существующего клиента.
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return ClientMapper.Map(entity);// Преобразование сущности в ViewModel.
        }
        public ClientViewModel Delete(long id) // Метод для удаления клиента из базы данных по идентификатору.
        {
            using (var context = new Context()) 
            {
                var clientToRemove = context.Clients.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null) {
                    context.Clients.Remove(clientToRemove);// Удаление клиента из базы данных.
                    context.SaveChanges();}
                return ClientMapper.Map(clientToRemove);// Преобразование удаленной сущности в ViewModel.
            }   
        }
        public ClientViewModel Add(ClientEntity entity) // Метод для добавления нового клиента в базу данных.
        {// Обрезка строковых полей от лишних пробелов.
            entity.Name = entity.Name.Trim();
            entity.SecondName = entity.SecondName.Trim();
            entity.MiddleName = entity.MiddleName.Trim();
            entity.DateOfBirth = entity.DateOfBirth.Trim();
            entity.Login = entity.Login.Trim();
            entity.Password = entity.Password.Trim();
            // Проверка наличия заполненных полей.
            if (string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.SecondName) || string.IsNullOrEmpty(entity.MiddleName) || string.IsNullOrEmpty(entity.DateOfBirth) || string.IsNullOrEmpty(entity.Login) || string.IsNullOrEmpty(entity.Password) )
            {
                throw new Exception("Не все поля заполнены");
            }
            using (var context = new Context()){
                context.Clients.Add(entity);// Добавление нового клиента в базу данных.
                context.SaveChanges();
            } 
            return ClientMapper.Map(entity);
        }
        public List<ClientViewModel> Search(string search)// Метод для поиска клиентов по имени в базе данных.
        {
            search = search.Trim().ToLower();  // Обрезка строки поиска и приведение к нижнему регистру.

            using (var context = new Context())
            {  // Поиск клиентов по имени в базе данных, включая связанные сущности, такие как скидки.
                var result = context.Clients.Include(x => x.Discount).Where(x => x.Name.ToLower().Contains(search) && x.Name.Length == search.Length).ToList();
                return ClientMapper.Map(result);
            }

        }
        public ClientViewModel Login(string login, string password)// Метод для аутентификации клиента по логину и паролю.
        {
            using (var context = new Context())
            {// Поиск клиента по логину и паролю в базе данных.
                var item = context.Clients.FirstOrDefault(x => x.Login == login && x.Password == password);

                return ClientMapper.Map(item);
            }
        }
    }
}
