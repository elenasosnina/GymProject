using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class SubscriptionTypeRepository
    {
        public SubscriptionTypeViewModel Update(SubscriptionTypeEntity entity)// Метод для обновления данных типа подписки в базе данных.
        {// Обрезка строковых полей от лишних пробелов.
            entity.Name = entity.Name.Trim();
            if (string.IsNullOrEmpty(entity.Name))// Проверка наличия заполненных полей.
            {
                throw new Exception("Имя пользователя не может быть пустым");
            }
            using (var context = new Context())
            {
                var existingClient = context.SubscriptionTypes.Find(entity.Id);

                if (existingClient != null)
                {// Обновление данных существующего типа подписки.
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return SubscriptionTypeMapper.Map(entity);
        }
        public SubscriptionTypeViewModel Delete(long id) // Метод для удаления типа подписки из базы данных по идентификатору
        {
            using (var context = new Context())
            {
                var clientToRemove = context.SubscriptionTypes.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.SubscriptionTypes.Remove(clientToRemove);// Удаление типа подписки из базы данных.
                    context.SaveChanges();
                }
                return SubscriptionTypeMapper.Map(clientToRemove);
            }
        }
        public SubscriptionTypeViewModel Add(SubscriptionTypeEntity entity)// Метод для добавления нового типа подписки в базу данных.
        {
            using (var context = new Context())
            {
                context.SubscriptionTypes.Add(entity);// Добавление нового типа подписки в базу данных.
                context.SaveChanges();
            }
            return SubscriptionTypeMapper.Map(entity);
        }
        public List<SubscriptionTypeViewModel> GetList()// Метод для получения списка типов подписок из базы данных.
        {
            using (var context = new Context())
            {
                var items = context.SubscriptionTypes.ToList();// Извлечение типов подписок из базы данных
                return SubscriptionTypeMapper.Map(items);
            }
        }
        public SubscriptionTypeViewModel GetById(long id)// Метод для получения типа подписки по идентификатору из базы данных.
        {
            using (var context = new Context())
            {
                var item = context.SubscriptionTypes.FirstOrDefault(x => x.Id == id);
                return SubscriptionTypeMapper.Map(item);// Преобразование сущности в ViewModel.
            }
        }

    }
}
