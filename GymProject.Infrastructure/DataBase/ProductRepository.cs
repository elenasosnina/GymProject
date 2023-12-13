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
        public ProductViewModel Update(ProductEntity entity)// Метод для обновления данных товаров в базе данных.
        {// Обрезка строковых полей от лишних пробелов.
            entity.Name = entity.Name.Trim();
            entity.Cost = entity.Cost;
            entity.Quantity = entity.Quantity;
            entity.ExpirationDate = entity.ExpirationDate.Trim();
            // Проверка наличия заполненных полей.
            if (string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.Cost.ToString()) || string.IsNullOrEmpty(entity.Quantity.ToString()) || string.IsNullOrEmpty(entity.ExpirationDate))
            {
                throw new Exception("Не все поля заполнены");
            }
            using (var context = new Context())
            {
                var existingClient = context.Products.Find(entity.Id);

                if (existingClient != null)
                {// Обновление данных существующего товара.
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return ProductMapper.Map(entity);// Преобразование сущности в ViewModel.
        }
        public ProductViewModel Delete(long id)// Метод для удаления товара из базы данных по идентификатору.
        {
            using (var context = new Context())
            {
                var clientToRemove = context.Products.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.Products.Remove(clientToRemove);// Удаление товара из базы данных.
                    context.SaveChanges();
                }
                return ProductMapper.Map(clientToRemove);
            }
        }
        public ProductViewModel Add(ProductEntity entity)// Метод для добавления нового товара в базу данных.
        {// Обрезка строковых полей от лишних пробелов.
            entity.Name = entity.Name.Trim();
            entity.Cost = entity.Cost;
            entity.Quantity = entity.Quantity;
            entity.ExpirationDate = entity.ExpirationDate.Trim();
            // Проверка наличия заполненных полей.
            if (string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.Cost.ToString()) || string.IsNullOrEmpty(entity.Quantity.ToString()) || string.IsNullOrEmpty(entity.ExpirationDate))
            {
                throw new Exception("Не все поля заполнены");
            }
            using (var context = new Context())
            {
                context.Products.Add(entity);// Добавление нового товара в базу данных.
                context.SaveChanges();
            }
            return ProductMapper.Map(entity);
        }
        public List<ProductViewModel> GetList()// Метод для получения списка товаров из базы данных.
        {
            using (var context = new Context())
            {
                var items = context.Products.Include(x => x.Product_category).ToList();// Извлечение товаров из базы данных, включая категорию товара
                return ProductMapper.Map(items);
            }
        }
        public ProductViewModel GetById(long id)// Метод для получения товаров по идентификатору из базы данных.
        {
            using (var context = new Context())
            {
                var item = context.Products.FirstOrDefault(x => x.Id == id);
                return ProductMapper.Map(item);
            }
        }
        public List<ProductViewModel> Search(string search)// Метод для поиска товаров по имени в базе данных.
        {
            search = search.Trim().ToLower();// Обрезка строки поиска и приведение к нижнему регистру.

            using (var context = new Context())
            {// Поиск товаров по имени в базе данных, включая категорю товара
                var result = context.Products.Include(x => x.Product_category).Where(x => x.Name.ToLower().Contains(search) && x.Name.Length == search.Length).ToList();
                return ProductMapper.Map(result);
            }

        }
    }
}
