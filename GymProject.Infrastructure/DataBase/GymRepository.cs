using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class GymRepository
    {
        public GymViewModel Update(GymEntity entity)// Метод для обновления данных зала в базе данных.
        {// Обрезка строковых полей от лишних пробелов.
            entity.Name = entity.Name.Trim();
            if (string.IsNullOrEmpty(entity.Name))// Проверка наличия заполненных полей.
            {
                throw new Exception("Имя пользователя не может быть пустым");
            }
            using (var context = new Context())
            {
                var existingClient = context.Gyms.Find(entity.Id);

                if (existingClient != null)
                {// Обновление данных существующего зала.
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return GymMapper.Map(entity);// Преобразование сущности в ViewModel.
        }
        public GymViewModel Delete(long id)// Метод для удаления зала из базы данных по идентификатору.
        {
            using (var context = new Context())
            {
                var clientToRemove = context.Gyms.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.Gyms.Remove(clientToRemove);// Удаление зала из базы данных.
                    context.SaveChanges();
                }
                return GymMapper.Map(clientToRemove);
            }
        }
        public GymViewModel Add(GymEntity entity)// Метод для добавления нового зала в базу данных.
        {
            using (var context = new Context())
            {
                context.Gyms.Add(entity);
                context.SaveChanges();
            }
            return GymMapper.Map(entity);
        }
        public List<GymViewModel> GetList()// Метод для получения списка залов из базы данных.
        {
            using (var context = new Context())
            {
                var items = context.Gyms.ToList();// Извлечение зала из базы данных, включая связанные сущности, такие как скидки.
                return GymMapper.Map(items);
            }
        }
        public GymViewModel GetById(long id)// Метод для получения зала по идентификатору из базы данных.
        {
            using (var context = new Context())
            {
                var item = context.Gyms.FirstOrDefault(x => x.Id == id);
                return GymMapper.Map(item);
            }
        }

    }
}
