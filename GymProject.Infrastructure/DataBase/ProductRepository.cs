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
    public class ProductRepository
    {
        public ProductViewModel Update(ProductEntity entity)
        {
            entity.Name = entity.Name.Trim();
            if (string.IsNullOrEmpty(entity.Name))
            {
                throw new Exception("Имя пользователя не может быть пустым");
            }
            using (var context = new Context())
            {
                var existingClient = context.Products.Find(entity.Id);

                if (existingClient != null)
                {
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return ProductMapper.Map(entity);
        }
        public ProductViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var clientToRemove = context.Products.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.Products.Remove(clientToRemove);
                    context.SaveChanges();
                }
                return ProductMapper.Map(clientToRemove);
            }
        }
        public ProductViewModel Add(ProductEntity entity)
        {
            using (var context = new Context())
            {
                context.Products.Add(entity);
                context.SaveChanges();
            }
            return ProductMapper.Map(entity);
        }
        public List<ProductViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Products.Include(x => x.Product_category).ToList();
                return ProductMapper.Map(items);
            }
        }
        public ProductViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Products.FirstOrDefault(x => x.Id == id);
                return ProductMapper.Map(item);
            }
        }

        
    }
}
