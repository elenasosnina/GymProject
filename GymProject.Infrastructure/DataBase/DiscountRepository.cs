using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class DiscountRepository
    {
        public List<DiscountEntity> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Discounts.ToList();
                return items;
            }
        }
        public DiscountEntity GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Discounts.FirstOrDefault(x => x.ID == id);
                return item;
            }
        }

    }
}
