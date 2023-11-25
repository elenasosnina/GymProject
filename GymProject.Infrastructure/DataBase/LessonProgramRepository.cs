using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class LessonProgramRepository
    {
        public List<LessonProgramEntity> GetList()
        {
            using (var context = new Context())
            {
                var items = context.LessonPrograms.ToList();
                return items;
            }
        }
        public LessonProgramEntity GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.LessonPrograms.FirstOrDefault(x => x.ID == id);
                return item;
            }
        }

    }
}
