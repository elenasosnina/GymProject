using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class HallRepository
    {
        public List<HallEntity> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Halls.ToList();
                return items;
            }
        }
        public HallEntity GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Halls.FirstOrDefault(x => x.ID == id);
                return item;
            }
        }

    }
}
