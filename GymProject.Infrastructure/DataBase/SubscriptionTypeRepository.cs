using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class SubscriptionTypeRepository
    {
        public List<SubscriptionTypeEntity> GetList()
        {
            using (var context = new Context())
            {
                var items = context.SubscriptionTypes.ToList();
                return items;
            }
        }
        public SubscriptionTypeEntity GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.SubscriptionTypes.FirstOrDefault(x => x.Id == id);
                return item;
            }
        }

    }
}
