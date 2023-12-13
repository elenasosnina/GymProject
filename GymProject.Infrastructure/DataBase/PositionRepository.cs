using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class PositionRepository
    {
        public PositionViewModel Update(PositionEntity entity)// Метод для обновления данных должности в базе данных.
        {
            using (var context = new Context())
            {
                var existingClient = context.Positions.Find(entity.Id);

                if (existingClient != null)
                {// Обновление данных существующего должности.
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return PositionMapper.Map(entity);// Преобразование сущности в ViewModel.
        }
        public PositionViewModel Delete(long id)// Метод для удаления должности из базы данных по идентификатору.
        {
            using (var context = new Context())
            {
                var clientToRemove = context.Positions.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.Positions.Remove(clientToRemove);// Удаление должности из базы данных.
                    context.SaveChanges();
                }
                return PositionMapper.Map(clientToRemove);
            }
        }
        public PositionViewModel Add(PositionEntity entity)// Метод для добавления новой должности в базу данных.
        {
            using (var context = new Context())
            {
                context.Positions.Add(entity);// Добавление новой должности в базу данных.
                context.SaveChanges();
            }
            return PositionMapper.Map(entity);
        }
        public List<PositionViewModel> GetList()// Метод для получения списка должностей из базы данных.
        {
            using (var context = new Context())
            {
                var items = context.Positions.ToList();// Извлечение должностей из базы данных, включая связанные сущности, такие как скидки.
                return PositionMapper.Map(items);
            }
        }
        public PositionViewModel GetById(long id)// Метод для получения должности по идентификатору из базы данных.
        {
            using (var context = new Context())
            {
                var item = context.Positions.FirstOrDefault(x => x.Id == id);
                return PositionMapper.Map(item);
            }
        }

    }
}
