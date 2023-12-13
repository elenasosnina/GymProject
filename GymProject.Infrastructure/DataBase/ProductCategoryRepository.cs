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
        public List<ProductCategoryViewModel> GetList()// Метод для получения списка категорий товара из базы данных.
        {
            using (var context = new Context())
            {
                var items = context.ProductCategories.ToList();// Извлечение категорий товара из базы данных
                return ProductCategoryMapper.Map(items);// Преобразование сущности в ViewModel.
            }
        }
        public ProductCategoryViewModel GetById(long id)// Метод для получения категории товара по идентификатору из базы данных.
        {
            using (var context = new Context())
            {
                var item = context.ProductCategories.FirstOrDefault(x => x.Id == id);
                return ProductCategoryMapper.Map(item);// Преобразование сущности в ViewModel.
            }
        }
        public ProductCategoryViewModel Update(ProductCategoryEntity entity)// Метод для обновления данных категории товара в базе данных.
        {// Обрезка строковых полей от лишних пробелов.
            using (var context = new Context())
            {
                var existingClient = context.ProductCategories.Find(entity.Id);
                // Проверка наличия заполненных полей.
                if (existingClient != null)
                {// Обновление данных существующего категории товара.
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return ProductCategoryMapper.Map(entity);
        }
        public ProductCategoryViewModel Delete(long id)// Метод для удаления категории товара из базы данных по идентификатору.
        {
            using (var context = new Context())
            {
                var clientToRemove = context.ProductCategories.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.ProductCategories.Remove(clientToRemove);// Удаление категории товара из базы данных.
                    context.SaveChanges();
                }
                return ProductCategoryMapper.Map(clientToRemove);
            }
        }
        public ProductCategoryViewModel Add(ProductCategoryEntity entity)// Метод для добавления новой категории товара в базу данных.
        {// Обрезка строковых полей от лишних пробелов.
            using (var context = new Context())
            {// Проверка наличия заполненных полей.
                context.ProductCategories.Add(entity);// Добавление новой категории товара в базу данных.
                context.SaveChanges();
            }
            return ProductCategoryMapper.Map(entity);
        }
    }
}
