using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class PositionRepository
    {
        public List<PositionEntity> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Positions.ToList();
                return items;
            }
        }
        public PositionEntity GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Positions.FirstOrDefault(x => x.ID == id);
                return item;
            }
        }

    }
}
