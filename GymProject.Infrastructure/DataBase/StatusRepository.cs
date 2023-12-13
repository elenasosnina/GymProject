using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class StatusRepository
    {
        public StatusViewModel Update(StatusEntity entity)// Метод для обновления данных статуса в базе данных.
        {
            using (var context = new Context())
            {
                var existingClient = context.Statuses.Find(entity.Id);

                if (existingClient != null)
                {// Обновление данных существующего статуса.
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return StatusMapper.Map(entity);// Преобразование сущности в ViewModel.
        }
        public StatusViewModel Delete(long id)// Метод для удаления статуса из базы данных по идентификатору.
        {
            using (var context = new Context())
            {
                var clientToRemove = context.Statuses.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.Statuses.Remove(clientToRemove);// Удаление статуса из базы данных.
                    context.SaveChanges();
                }
                return StatusMapper.Map(clientToRemove);
            }
        }
        public StatusViewModel Add(StatusEntity entity)// Метод для добавления нового статуса в базу данных.
        {
            using (var context = new Context())
            {
                context.Statuses.Add(entity);// Добавление нового статуса в базу данных.
                context.SaveChanges();
            }
            return StatusMapper.Map(entity);
        }
        public List<StatusViewModel> GetList()// Метод для получения списка статусов из базы данных.
        {
            using (var context = new Context())
            {
                var items = context.Statuses.ToList();// Извлечение статуса из базы данных
                return StatusMapper.Map(items);
            }
        }
        public StatusViewModel GetById(long id)// Метод для получения статуса по идентификатору из базы данных.
        {
            using (var context = new Context())
            {
                var item = context.Statuses.FirstOrDefault(x => x.Id == id);
                return StatusMapper.Map(item);
            }
        }

    }
}
