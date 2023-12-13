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
        public LessonViewModel Update(LessonEntity entity)// Метод для обновления данных занятий в базе данных.
        {// Обрезка строковых полей от лишних пробелов.
            entity.DateAndTime = entity.DateAndTime.Trim();

            if (string.IsNullOrEmpty(entity.DateAndTime)) // Проверка наличия заполненных полей.
            {
                throw new Exception("Не все поля заполнены");
            }
            using (var context = new Context())
            {
                var existingClient = context.Lessons.Find(entity.Id);

                if (existingClient != null)
                {// Обновление данных существующего занятия.
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return LessonMapper.Map(entity);// Преобразование сущности в ViewModel.
        }
        public LessonViewModel Delete(long id)// Метод для удаления занятия из базы данных по идентификатору.
        {
            using (var context = new Context())
            {
                var clientToRemove = context.Lessons.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.Lessons.Remove(clientToRemove);// Удаление занятия из базы данных.
                    context.SaveChanges();
                }
                return LessonMapper.Map(clientToRemove);
            }
        }
        public LessonViewModel Add(LessonEntity entity) // Метод для добавления нового занятия в базу данных.
        {// Обрезка строковых полей от лишних пробелов.
            entity.DateAndTime = entity.DateAndTime.Trim();

            if (string.IsNullOrEmpty(entity.DateAndTime)) // Проверка наличия заполненных полей.
            {
                throw new Exception("Не все поля заполнены");
            }
            using (var context = new Context())
            {
                context.Lessons.Add(entity);// Добавление нового занятия в базу данных.
                context.SaveChanges();
            }
            return LessonMapper.Map(entity);
        }
        public List<LessonViewModel> GetList()// Метод для получения списка занятий из базы данных.
        {
            using (var context = new Context())
            { // Извлечение занятий из базы данных,  включая сущности зала, тренажерного зала, программы занятий, абонемента включающего его тип
                var items = context.Lessons.Include(x => x.Hall).Include(x => x.Gym).Include(x => x.Lesson_programs).Include(x => x.Subscription).Include(x => x.Subscription.Client).ToList();
               
               return LessonMapper.Map(items);
            }
        }
        public LessonViewModel GetById(long id)// Метод для получения занятий по идентификатору из базы данных.
        {
            using (var context = new Context())
            {
                var item = context.Lessons.FirstOrDefault(x => x.Id == id);
                return LessonMapper.Map(item);
            }
        }
        public List<LessonViewModel> Search(string search)// Метод для поиска занятий по имени в базе данных.
        {
            search = search.Trim().ToLower();// Обрезка строки поиска и приведение к нижнему регистру.

            using (var context = new Context())
            {// Поиск занятий по имени в базе данных, включая сущности зала, тренажерного зала, программы занятий, абонемента включающего его тип
                var result = context.Lessons.Include(x => x.Hall).Include(x => x.Gym).Include(x => x.Lesson_programs).Include(x => x.Subscription.Subscription_type).Include(x => x.Subscription.Client).Where(x => x.DateAndTime.Contains(search) && x.DateAndTime.Length == search.Length).ToList();
                return LessonMapper.Map(result);
            }

        }
    }
}
