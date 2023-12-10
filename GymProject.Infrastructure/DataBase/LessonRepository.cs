using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GymProject.Infrastructure.DataBase
{
    public class LessonRepository
    {
        public LessonViewModel Update(LessonEntity entity)
        {
            entity.DateAndTime = entity.DateAndTime.Trim();

            if (string.IsNullOrEmpty(entity.DateAndTime))
            {
                throw new Exception("Не все поля заполнены");
            }
            using (var context = new Context())
            {
                var existingClient = context.Lessons.Find(entity.Id);

                if (existingClient != null)
                {
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return LessonMapper.Map(entity);
        }
        public LessonViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var clientToRemove = context.Lessons.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.Lessons.Remove(clientToRemove);
                    context.SaveChanges();
                }
                return LessonMapper.Map(clientToRemove);
            }
        }
        public LessonViewModel Add(LessonEntity entity)
        {
            entity.DateAndTime = entity.DateAndTime.Trim();

            if (string.IsNullOrEmpty(entity.DateAndTime))
            {
                throw new Exception("Не все поля заполнены");
            }
            using (var context = new Context())
            {
                context.Lessons.Add(entity);
                context.SaveChanges();
            }
            return LessonMapper.Map(entity);
        }
        public List<LessonViewModel> GetList()
        {
            using (var context = new Context())
            { 
                var items = context.Lessons.Include(x => x.Hall).Include(x => x.Gym).Include(x => x.Lesson_programs).Include(x => x.Subscription).Include(x => x.Subscription.Client).ToList();
               
               return LessonMapper.Map(items);
            }
        }
        public LessonViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Lessons.FirstOrDefault(x => x.Id == id);
                return LessonMapper.Map(item);
            }
        }
        public List<LessonViewModel> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.Lessons.Include(x => x.Hall).Include(x => x.Gym).Include(x => x.Lesson_programs).Include(x => x.Subscription.Subscription_type).Include(x => x.Subscription.Client).Where(x => x.DateAndTime.Contains(search) && x.DateAndTime.Length == search.Length).ToList();
                return LessonMapper.Map(result);
            }

        }
    }
}
