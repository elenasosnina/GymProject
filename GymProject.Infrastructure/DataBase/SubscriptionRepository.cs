using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GymProject.Infrastructure.DataBase
{
    public class SubscriptionRepository
    {
        public SubscriptionViewModel Update(SubscriptionEntity entity)// Метод для обновления данных подписки в базе данных.
        {// Обрезка строковых полей от лишних пробелов.
            entity.ValidityStartDate = entity.ValidityStartDate.Trim();
            entity.ValidityExpirationDate = entity.ValidityExpirationDate.Trim();
            if (string.IsNullOrEmpty(entity.ValidityStartDate) || string.IsNullOrEmpty(entity.ValidityExpirationDate))// Проверка наличия заполненных полей.
            {
                throw new Exception("Имя пользователя не может быть пустым");
            }
            using (var context = new Context())
            {
                var existingClient = context.Subscriptions.Find(entity.Id);

                if (existingClient != null)
                {// Обновление данных существующей подписки .
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return SubscriptionMapper.Map(entity);
        }
        public SubscriptionViewModel Delete(long id)// Метод для удаления подписки из базы данных по идентификатору.
        {
            using (var context = new Context())
            {
                var clientToRemove = context.Subscriptions.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.Subscriptions.Remove(clientToRemove);// Удаление подписки из базы данных.
                    context.SaveChanges();
                }
                return SubscriptionMapper.Map(clientToRemove);// Преобразование удаленной сущности в ViewModel.
            }
        }
        public SubscriptionViewModel Add(SubscriptionEntity entity)// Метод для добавления новой подписки в базу данных.
        {// Обрезка строковых полей от лишних пробелов.
            entity.ValidityStartDate = entity.ValidityStartDate.Trim();
            entity.ValidityExpirationDate = entity.ValidityExpirationDate.Trim();
            if (string.IsNullOrEmpty(entity.ValidityStartDate) || string.IsNullOrEmpty(entity.ValidityExpirationDate))// Проверка наличия заполненных полей.
            {
                throw new Exception("Имя пользователя не может быть пустым");
            }
            using (var context = new Context())
            {
                context.Subscriptions.Add(entity);// Добавление новой подписки в базу данных.
                context.SaveChanges();
            }
            return SubscriptionMapper.Map(entity);
        }
        public List<SubscriptionViewModel> GetList()// Метод для получения списка подписок из базы данных.
        {
            using (var context = new Context())
            {
                var items = context.Subscriptions.Include(x => x.Status).Include(x => x.Client).Include(x => x.Subscription_type).ToList();// Извлечение подписок из базы данных, включая связанные сущности, такие как клиенты,статус, типа подписки.
                return SubscriptionMapper.Map(items);// Преобразование сущностей в ViewModel.
            }
        }
        public SubscriptionViewModel GetById(long id)// Метод для получения подписки по идентификатору из базы данных.
        {
            using (var context = new Context())
            {
                var item = context.Subscriptions.FirstOrDefault(x => x.Id == id);
                return SubscriptionMapper.Map(item);
            }
        }
        public List<SubscriptionViewModel> Search(string search)// Метод для поиска подписки по имени в базе данных.
        {
            search = search.Trim().ToLower();// Обрезка строки поиска и приведение к нижнему регистру.

            using (var context = new Context())
            {// Поиск подписки по дате начала действия в базе данных, включая сущности клиентов, статуса, типа абонемента
                var result = context.Subscriptions.Include(x => x.Client).Include(x => x.Status).Include(x => x.Subscription_type).Where(x => x.ValidityStartDate.ToLower().Contains(search) && x.ValidityStartDate.Length == search.Length).ToList();
                return SubscriptionMapper.Map(result);
            }

        }
    }
}
