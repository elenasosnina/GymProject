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
        public SubscriptionTypeViewModel Update(SubscriptionTypeEntity entity)
        {
            entity.Name = entity.Name.Trim();
            if (string.IsNullOrEmpty(entity.Name))
            {
                throw new Exception("Имя пользователя не может быть пустым");
            }
            using (var context = new Context())
            {
                var existingClient = context.SubscriptionTypes.Find(entity.Id);

                if (existingClient != null)
                {
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return SubscriptionTypeMapper.Map(entity);
        }
        public SubscriptionTypeViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var clientToRemove = context.SubscriptionTypes.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.SubscriptionTypes.Remove(clientToRemove);
                    context.SaveChanges();
                }
                return SubscriptionTypeMapper.Map(clientToRemove);
            }
        }
        public SubscriptionTypeViewModel Add(SubscriptionTypeEntity entity)
        {
            using (var context = new Context())
            {
                context.SubscriptionTypes.Add(entity);
                context.SaveChanges();
            }
            return SubscriptionTypeMapper.Map(entity);
        }
        public List<SubscriptionTypeViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.SubscriptionTypes.ToList();
                return SubscriptionTypeMapper.Map(items);
            }
        }
        public SubscriptionTypeViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.SubscriptionTypes.FirstOrDefault(x => x.Id == id);
                return SubscriptionTypeMapper.Map(item);
            }
        }

    }
}
