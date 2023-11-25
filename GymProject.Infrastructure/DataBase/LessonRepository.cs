using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class LessonRepository
    {
        public List<LessonEntity> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Lessons.ToList();
                return items;
            }
        }
        public LessonEntity GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Lessons.FirstOrDefault(x => x.ID == id);
                return item;
            }
        }

    }
}
