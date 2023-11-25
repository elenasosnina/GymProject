using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    internal class StatusRepository
    {
        public List<StatusEntity> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Statuss.ToList();
                return items;
            }
        }
        public StatusEntity GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Statuss.FirstOrDefault(x => x.Id == id);
                return item;
            }
        }

    }
}
