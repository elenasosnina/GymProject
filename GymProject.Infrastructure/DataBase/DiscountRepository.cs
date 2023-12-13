using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class DiscountRepository
    {
        public List<DiscountViewModel> GetList()// Метод для получения списка скидок из базы данных.
        {
            using (var context = new Context())
            {
                var items = context.Discounts.ToList();// Извлечение скидок из базы данных
                return DiscountMapper.Map(items);// Преобразование сущностей в ViewModel
            }
        }
        public DiscountViewModel GetById(long id)// Метод для получения скидок по идентификатору из базы данных.
        {
            using (var context = new Context())
            {
                var item = context.Discounts.FirstOrDefault(x => x.Id == id);
                return DiscountMapper.Map(item);
            }
        }
        public DiscountViewModel Update(DiscountEntity entity)// Метод для обновления данных скидок в базе данных.
        {
            
            using (var context = new Context())
            {
                var existingClient = context.Discounts.Find(entity.Id);

                if (existingClient != null)
                {// Обновление данных существующеq скидки.
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return DiscountMapper.Map(entity);
        }
        public DiscountViewModel Delete(long id)// Метод для удаления скидок из базы данных по идентификатору.
        {
            using (var context = new Context())
            {
                var clientToRemove = context.Discounts.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.Discounts.Remove(clientToRemove);// Удаление скидок из базы данных.
                    context.SaveChanges();
                }
                return DiscountMapper.Map(clientToRemove);
            }
        }
        public DiscountViewModel Add(DiscountEntity entity)// Метод для добавления новой скидки в базу данных.
        {
            using (var context = new Context())
            {
                context.Discounts.Add(entity);// Добавление новой скидки в базу данных.
                context.SaveChanges();
            }
            return DiscountMapper.Map(entity);
        }

    }
}
