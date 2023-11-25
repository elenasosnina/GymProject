using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class SubscriptionRepository
    {
        public List<SubscriptionEntity> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Subscriptions.ToList();
                return items;
            }
        }
        public SubscriptionEntity GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Subscriptions.FirstOrDefault(x => x.Id == id);
                return item;
            }
        }

    }
}
