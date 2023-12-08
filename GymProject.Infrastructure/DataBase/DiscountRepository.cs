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
        public List<DiscountViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Discounts.ToList();
                return DiscountMapper.Map(items);
            }
        }
        public DiscountViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Discounts.FirstOrDefault(x => x.Id == id);
                return DiscountMapper.Map(item);
            }
        }
        public DiscountViewModel Update(DiscountEntity entity)
        {
            //entity.Name = entity.Name.Trim();
            //if (string.IsNullOrEmpty(entity.Name))
            //{
            //    throw new Exception("Имя пользователя не может быть пустым");
            //}
            using (var context = new Context())
            {
                var existingClient = context.Discounts.Find(entity.Id);

                if (existingClient != null)
                {
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return DiscountMapper.Map(entity);
        }
        public DiscountViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var clientToRemove = context.Discounts.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.Discounts.Remove(clientToRemove);
                    context.SaveChanges();
                }
                return DiscountMapper.Map(clientToRemove);
            }
        }
        public DiscountViewModel Add(DiscountEntity entity)
        {
            using (var context = new Context())
            {
                context.Discounts.Add(entity);
                context.SaveChanges();
            }
            return DiscountMapper.Map(entity);
        }

    }
}
