using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GymProject.Infrastructure.Mappers
{
    public static class ProductMapper
    {
        public static ProductViewModel Map(ProductEntity entity)// Метод для отображения сущности ProductEntity на представление ProductViewModel.
        {
            if (entity == null)// Проверка наличия сущности.
                return null;
            var viewModel = new ProductViewModel// Создание объекта представления и копирование данных из сущности.
            {
                Id = entity.Id,
                Name = entity.Name,
                Cost = entity.Cost,
                Quantity = entity.Quantity,
                ExpirationDate = entity.ExpirationDate,
                ProductCategoryId = entity.ProductCategoryId,
                ProductCategory = ProductCategoryMapper.Map(entity.Product_category),
            };
            return viewModel;
        }

        public static ProductEntity Map(ProductViewModel viewModel)// Метод для отображения представления ProductViewModel на сущность ProductEntity.
        {
            var entity = new ProductEntity// Создание объекта сущности и копирование данных из представления.
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Cost = viewModel.Cost,
                Quantity = viewModel.Quantity,
                ExpirationDate = viewModel.ExpirationDate,
                ProductCategoryId = viewModel.ProductCategoryId,
                Product_category = ProductCategoryMapper.Map(viewModel.ProductCategory),
            };

            return entity;
        }

        public static List<ProductViewModel> Map(List<ProductEntity> entities)// Метод для отображения списка сущностей ProductEntity на список представлений ProductViewModel.
        {
            var viewModels = entities.Select(x => Map(x)).ToList();// Преобразование каждой сущности в соответствующее представление и создание списка представлений
            return viewModels;
        }
    }
}
