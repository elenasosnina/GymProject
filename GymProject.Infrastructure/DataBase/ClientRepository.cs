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

        public List<ClientViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Clients.Include(x => x.Discount).ToList();
                return ClientMapper.Map(items);
            }
        }
        public ClientViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Clients.FirstOrDefault(x => x.Id == id);

                return ClientMapper.Map(item);
            }

        }
        public ClientViewModel Update(ClientEntity entity)
        {
            entity.Name = entity.Name.Trim();
            entity.SecondName = entity.SecondName.Trim();
            entity.MiddleName = entity.MiddleName.Trim();
            entity.DateOfBirth = entity.DateOfBirth.Trim();
            entity.Login = entity.Login.Trim();
            entity.Password = entity.Password.Trim();
            if (string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.SecondName) || string.IsNullOrEmpty(entity.MiddleName) || string.IsNullOrEmpty(entity.DateOfBirth) || string.IsNullOrEmpty(entity.Login) || string.IsNullOrEmpty(entity.Password))
            {
                throw new Exception("Не все поля заполнены");
            }

            using (var context = new Context())
            {
                var existingClient = context.Clients.Find(entity.Id);

                if (existingClient != null)
                {
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return ClientMapper.Map(entity);
        }
        public ClientViewModel Delete(long id)
        {
            using (var context = new Context()) 
            {
                var clientToRemove = context.Clients.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null) {
                    context.Clients.Remove(clientToRemove);
                    context.SaveChanges();}
                return ClientMapper.Map(clientToRemove);
            }   
        }
        public ClientViewModel Add(ClientEntity entity)
        {
            entity.Name = entity.Name.Trim();
            entity.SecondName = entity.SecondName.Trim();
            entity.MiddleName = entity.MiddleName.Trim();
            entity.DateOfBirth = entity.DateOfBirth.Trim();
            entity.Login = entity.Login.Trim();
            entity.Password = entity.Password.Trim();
            if (string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.SecondName) || string.IsNullOrEmpty(entity.MiddleName) || string.IsNullOrEmpty(entity.DateOfBirth) || string.IsNullOrEmpty(entity.Login) || string.IsNullOrEmpty(entity.Password) )
            {
                throw new Exception("Не все поля заполнены");
            }
            using (var context = new Context()){
                context.Clients.Add(entity);
                context.SaveChanges();
            } 
            return ClientMapper.Map(entity);
        }
        public List<ClientEntity> Search(string search)
        {
            search = search.Trim();

            using (var context = new Context())
            {
                var result = context.Clients.Include(x => x.Discount).Where(x => x.Name.Contains(search) && x.Name.Length == search.Length).ToList();
                return result;
            }

        }
        public ClientViewModel Login(string login, string password)
        {
            using (var context = new Context())
            {
                var item = context.Clients.FirstOrDefault(x => x.Login == login && x.Password == password);

                return ClientMapper.Map(item);
            }
        }
    }
}
