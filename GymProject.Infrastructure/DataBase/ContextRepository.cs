using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class ContextRepository
    {
        public List<ContextEntity> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Contexts.ToList();
                return items;
            }
        }
        public ContextEntity GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Contexts.FirstOrDefault(x => x.ID == id);
                return item;
            }
        }

    }
}
