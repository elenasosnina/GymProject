using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.Mappers
{
    public static class ProductCategoryMapper
    {
        public static ProductCategoryViewModel Map(ProductCategoryEntity entity)// Метод для отображения сущности ProductCategoryEntity на представление ProductCategoryViewModel.
        {
            if (entity == null) // Проверка наличия сущности.
                return null;
            var viewModel = new ProductCategoryViewModel// Создание объекта представления и копирование данных из сущности.
            {
                Id = entity.Id,
                Name = entity.Name,
                
            };
            return viewModel;
        }
        public static ProductCategoryEntity Map(ProductCategoryViewModel viewModel) // Метод для отображения представления ProductCategoryViewModel на сущность ProductCategoryEntity.
        {
            if (viewModel == null)// Проверка наличия сущности.
                return null;
            var entity = new ProductCategoryEntity// Создание объекта сущности и копирование данных из представления.
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                
            };
            return entity;
        }

        public static List<ProductCategoryViewModel> Map(List<ProductCategoryEntity> entities)// Метод для отображения списка сущностей ProductCategoryEntity на список представлений ProductCategoryViewModel.
        {
            var viewModels = entities.Select(x => Map(x)).ToList();// Преобразование каждой сущности в соответствующее представление и создание списка представлений
            return viewModels;
        }
    }
}
