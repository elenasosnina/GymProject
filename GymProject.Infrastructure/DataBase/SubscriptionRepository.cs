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
        public SubscriptionViewModel Update(SubscriptionEntity entity)
        {
            entity.ValidityStartDate = entity.ValidityStartDate.Trim();
            entity.ValidityExpirationDate = entity.ValidityExpirationDate.Trim();
            if (string.IsNullOrEmpty(entity.ValidityStartDate) || string.IsNullOrEmpty(entity.ValidityExpirationDate))
            {
                throw new Exception("Имя пользователя не может быть пустым");
            }
            using (var context = new Context())
            {
                var existingClient = context.Subscriptions.Find(entity.Id);

                if (existingClient != null)
                {
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return SubscriptionMapper.Map(entity);
        }
        public SubscriptionViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var clientToRemove = context.Subscriptions.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.Subscriptions.Remove(clientToRemove);
                    context.SaveChanges();
                }
                return SubscriptionMapper.Map(clientToRemove);
            }
        }
        public SubscriptionViewModel Add(SubscriptionEntity entity)
        {
            entity.ValidityStartDate = entity.ValidityStartDate.Trim();
            entity.ValidityExpirationDate = entity.ValidityExpirationDate.Trim();
            if (string.IsNullOrEmpty(entity.ValidityStartDate) || string.IsNullOrEmpty(entity.ValidityExpirationDate))
            {
                throw new Exception("Имя пользователя не может быть пустым");
            }
            using (var context = new Context())
            {
                context.Subscriptions.Add(entity);
                context.SaveChanges();
            }
            return SubscriptionMapper.Map(entity);
        }
        public List<SubscriptionViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Subscriptions.Include(x => x.Status).Include(x => x.Client).Include(x => x.Subscription_type).ToList();
                return SubscriptionMapper.Map(items);
            }
        }
        public SubscriptionViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Subscriptions.FirstOrDefault(x => x.Id == id);
                return SubscriptionMapper.Map(item);
            }
        }

    }
}
