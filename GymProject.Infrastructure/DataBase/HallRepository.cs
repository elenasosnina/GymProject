using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class HallRepository
    {
        public HallViewModel Update(HallEntity entity)// Метод для обновления данных тренажерного зала  в базе данных.
        {
            using (var context = new Context())
            {
                var existingClient = context.Halls.Find(entity.Id);

                if (existingClient != null)
                {// Обновление данных существующего тренажерного зала .
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return HallMapper.Map(entity);// Преобразование сущности в ViewModel.
        }
        public HallViewModel Delete(long id)// Метод для удаления тренажерного зала  из базы данных по идентификатору.
        {
            using (var context = new Context())
            {
                var clientToRemove = context.Halls.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.Halls.Remove(clientToRemove);// Удаление тренажерного зала  из базы данных.
                    context.SaveChanges();
                }
                return HallMapper.Map(clientToRemove);
            }
        }
        public HallViewModel Add(HallEntity entity)// Метод для добавления нового тренажерного зала  в базу данных.
        {
            using (var context = new Context())
            {
                context.Halls.Add(entity);// Добавление нового тренажерного зала  в базу данных.
                context.SaveChanges();
            }
            return HallMapper.Map(entity);
        }
        public List<HallViewModel> GetList()// Метод для получения списка тренажерных залов  из базы данных.
        {
            using (var context = new Context())
            {
                var items = context.Halls.ToList();// Извлечение тренажерного зала  из базы данных
                return HallMapper.Map(items);
            }
        }
        public HallViewModel GetById(long id)// Метод для получения тренажерного зала по идентификатору из базы данных.
        {
            using (var context = new Context())
            {
                var item = context.Halls.FirstOrDefault(x => x.Id == id);
                return HallMapper.Map(item);
            }
        }
    }
}
