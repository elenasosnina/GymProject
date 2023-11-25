using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class GymRepository
    {
        public List<GymEntity> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Gyms.ToList();
                return items;
            }
        }
        public GymEntity GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Gyms.FirstOrDefault(x => x.ID == id);
                return item;
            }
        }

    }
}
