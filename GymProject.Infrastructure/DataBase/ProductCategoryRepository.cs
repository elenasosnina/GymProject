using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class ProductCategoryRepository
    {
        public List<ProductCategoryViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.ProductCategories.ToList();
                return ProductCategoryMapper.Map(items);
            }
        }
        public ProductCategoryViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.ProductCategories.FirstOrDefault(x => x.Id == id);
                return ProductCategoryMapper.Map(item);
            }
        }
        public ProductCategoryViewModel Update(ProductCategoryEntity entity)
        {
            //entity.Name = entity.Name.Trim();
            //if (string.IsNullOrEmpty(entity.Name))
            //{
            //    throw new Exception("Имя пользователя не может быть пустым");
            //}
            using (var context = new Context())
            {
                var existingClient = context.ProductCategories.Find(entity.Id);

                if (existingClient != null)
                {
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return ProductCategoryMapper.Map(entity);
        }
        public ProductCategoryViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var clientToRemove = context.ProductCategories.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.ProductCategories.Remove(clientToRemove);
                    context.SaveChanges();
                }
                return ProductCategoryMapper.Map(clientToRemove);
            }
        }
        public ProductCategoryViewModel Add(ProductCategoryEntity entity)
        {
            using (var context = new Context())
            {
                context.ProductCategories.Add(entity);
                context.SaveChanges();
            }
            return ProductCategoryMapper.Map(entity);
        }
    }
}
