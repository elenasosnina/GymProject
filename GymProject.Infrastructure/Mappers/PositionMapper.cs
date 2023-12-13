using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.Mappers
{
    public static class PositionMapper
    {
        public static PositionViewModel Map(PositionEntity entity)// Метод для отображения сущности PositionEntity на представление PositionViewModel.
        {
            if (entity == null) // Проверка наличия сущности.
                return null;
            var viewModel = new PositionViewModel// Создание объекта представления и копирование данных из сущности.
            {
                Id = entity.Id,
                Title = entity.Title,
                Salary = entity.Salary,
                WorkSchedule = entity.WorkSchedule,


            };
            return viewModel;
        }
        public static PositionEntity Map(PositionViewModel viewModel) // Метод для отображения представления PositionViewModel на сущность PositionEntity.
        {
            if (viewModel == null)// Проверка наличия сущности.
                return null;
            var entity = new PositionEntity// Создание объекта сущности и копирование данных из представления.
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Salary = viewModel.Salary,
                WorkSchedule = viewModel.WorkSchedule,
            };

            return entity;
        }
        public static List<PositionViewModel> Map(List<PositionEntity> entities)// Метод для отображения списка сущностей PositionEntity на список представлений PositionViewModel.
        {
            var viewModels = entities.Select(x => Map(x)).ToList();// Преобразование каждой сущности в соответствующее представление и создание списка представлений
            return viewModels;
        }
    }
}
