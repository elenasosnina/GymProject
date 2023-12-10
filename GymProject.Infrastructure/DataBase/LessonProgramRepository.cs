using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class LessonProgramRepository
    {
        public LessonProgramViewModel Update(LessonProgramEntity entity)
        {

            entity.Name = entity.Name.Trim();
            entity.Description = entity.Description.Trim();
            entity.ProgramDuration = entity.ProgramDuration;

            if (string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.Description) || string.IsNullOrEmpty(entity.ProgramDuration.ToString()))
            {
                throw new Exception("Не все поля заполнены");
            }
            using (var context = new Context())
            {
                var existingClient = context.LessonPrograms.Find(entity.Id);

                if (existingClient != null)
                {
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return LessonProgramMapper.Map(entity);
        }
        public LessonProgramViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var clientToRemove = context.LessonPrograms.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.LessonPrograms.Remove(clientToRemove);
                    context.SaveChanges();
                }
                return LessonProgramMapper.Map(clientToRemove);
            }
        }
        public LessonProgramViewModel Add(LessonProgramEntity entity)
        {

            entity.Name = entity.Name.Trim();
            entity.Description = entity.Description.Trim();
            entity.ProgramDuration = entity.ProgramDuration;

            if (string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.Description) || string.IsNullOrEmpty(entity.ProgramDuration.ToString()))
            {
                throw new Exception("Не все поля заполнены");
            }
            using (var context = new Context())
            {
                context.LessonPrograms.Add(entity);
                context.SaveChanges();
            }
            return LessonProgramMapper.Map(entity);
        }
        public List<LessonProgramViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.LessonPrograms.ToList();
                return LessonProgramMapper.Map(items);
            }
        }
        public LessonProgramViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.LessonPrograms.FirstOrDefault(x => x.Id == id);
                return LessonProgramMapper.Map(item);
            }
        }
        public List<LessonProgramViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.LessonPrograms.Where(x => x.Name.ToLower().Contains(search) && x.Name.Length == search.Length).ToList();
                return LessonProgramMapper.Map(result);
            }

        }
    }
}
