﻿using GymProject.Infrastructure.Mappers;
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
            entity.Cost = entity.Cost;
            entity.Quantity = entity.Quantity;
            entity.ExpirationDate = entity.ExpirationDate.Trim();

            if (string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.Cost.ToString()) || string.IsNullOrEmpty(entity.Quantity.ToString()) || string.IsNullOrEmpty(entity.ExpirationDate))
            {
                throw new Exception("Не все поля заполнены");
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
            entity.Name = entity.Name.Trim();
            entity.Cost = entity.Cost;
            entity.Quantity = entity.Quantity;
            entity.ExpirationDate = entity.ExpirationDate.Trim();

            if (string.IsNullOrEmpty(entity.Name) || string.IsNullOrEmpty(entity.Cost.ToString()) || string.IsNullOrEmpty(entity.Quantity.ToString()) || string.IsNullOrEmpty(entity.ExpirationDate))
            {
                throw new Exception("Не все поля заполнены");
            }
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
        public List<ProductEntity> Search(string search)
        {
            search = search.Trim().ToLower();

            using (var context = new Context())
            {
                var result = context.Products.Include(x => x.Product_category).Where(x => x.Name.Contains(search) && x.Name.Length == search.Length).ToList();
                return result;
            }

        }
    }
}
