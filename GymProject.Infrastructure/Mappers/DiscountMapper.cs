using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.Mappers
{
    public static class DiscountMapper
    {
        public static DiscountViewModel Map(DiscountEntity entity)// Метод для отображения сущности DiscountEntity на представление DiscountViewModel.
        {
            if (entity == null)// Проверка наличия сущности.
                return null;

            var viewModel = new DiscountViewModel// Создание объекта представления и копирование данных из сущности.
            {
                Id = entity.Id,
                Name = entity.Name,
                Value = entity.Value,
            };
            return viewModel;
        }
        public static DiscountEntity Map(DiscountViewModel viewModel)// Метод для отображения представления DiscountViewModel на сущность DiscountEntity.
        {
            if (viewModel == null)// Проверка наличия сущности.
                return null;

            var entity = new DiscountEntity// Создание объекта представления и копирование данных из сущности.
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Value = viewModel.Value,
            };
            return entity;
        }

        public static List<DiscountViewModel> Map(List<DiscountEntity> entities)// Метод для отображения списка сущностей DiscountEntity на список представлений DiscountViewModel.
        {
            var viewModels = entities.Select(x => Map(x)).ToList();// Преобразование каждой сущности в соответствующее представление и создание списка представлений
            return viewModels;
        }
    }
}
