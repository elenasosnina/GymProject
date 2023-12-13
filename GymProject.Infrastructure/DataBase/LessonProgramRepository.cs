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
        public LessonProgramViewModel Update(LessonProgramEntity entity)// Метод для обновления данных программы занятий в базе данных.
        {// Обрезка строковых полей от лишних пробелов.

            entity.Name = entity.Name.Trim();
            entity.Description = entity.Description.Trim();
            entity.ProgramDuration = entity.ProgramDuration;
            // Проверка наличия заполненных полей.
            if (string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.Description) || string.IsNullOrEmpty(entity.ProgramDuration.ToString()))
            {
                throw new Exception("Не все поля заполнены");
            }
            using (var context = new Context())
            {
                var existingClient = context.LessonPrograms.Find(entity.Id);

                if (existingClient != null)
                {// Обновление данных существующей программы занятий.
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return LessonProgramMapper.Map(entity);// Преобразование сущности в ViewModel.
        }
        public LessonProgramViewModel Delete(long id)// Метод для удаления программы занятий из базы данных по идентификатору.
        {
            using (var context = new Context())
            {
                var clientToRemove = context.LessonPrograms.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.LessonPrograms.Remove(clientToRemove);// Удаление программы занятий из базы данных.
                    context.SaveChanges();
                }
                return LessonProgramMapper.Map(clientToRemove);
            }
        }
        public LessonProgramViewModel Add(LessonProgramEntity entity)// Метод для добавления новой программы занятий в базу данных.
        {// Обрезка строковых полей от лишних пробелов.
            entity.Name = entity.Name.Trim();
            entity.Description = entity.Description.Trim();
            entity.ProgramDuration = entity.ProgramDuration;
            // Проверка наличия заполненных полей.
            if (string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.Description) || string.IsNullOrEmpty(entity.ProgramDuration.ToString()))
            {
                throw new Exception("Не все поля заполнены");
            }
            using (var context = new Context())
            {
                context.LessonPrograms.Add(entity);// Добавление новой программы занятий в базу данных.
                context.SaveChanges();
            }
            return LessonProgramMapper.Map(entity);
        }
        public List<LessonProgramViewModel> GetList()// Метод для получения списка программ занятий из базы данных.
        {
            using (var context = new Context())
            {
                var items = context.LessonPrograms.ToList();// Извлечение программы занятий из базы данных
                return LessonProgramMapper.Map(items);
            }
        }
        public LessonProgramViewModel GetById(long id)// Метод для получения программы занятий по идентификатору из базы данных.
        {
            using (var context = new Context())
            {
                var item = context.LessonPrograms.FirstOrDefault(x => x.Id == id);
                return LessonProgramMapper.Map(item);
            }
        }
        public List<LessonProgramViewModel> Search(string search)// Метод для поиска программы занятий по имени в базе данных.
        {
            search = search.Trim().ToLower();// Обрезка строки поиска и приведение к нижнему регистру.

            using (var context = new Context())
            {// Поиск программы занятий по имени в базе данных
                var result = context.LessonPrograms.Where(x => x.Name.ToLower().Contains(search) && x.Name.Length == search.Length).ToList();
                return LessonProgramMapper.Map(result);
            }

        }
    }
}
