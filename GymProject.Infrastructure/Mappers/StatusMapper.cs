using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.Mappers
{
    public static class StatusMapper
    {
        public static StatusViewModel Map(StatusEntity entity)// Метод для отображения сущности StatusEntity на представление StatusViewModel.
        {
            if (entity == null)// Проверка наличия сущности.
                return null;
            var viewModel = new StatusViewModel// Создание объекта представления и копирование данных из сущности.
            {
                Id = entity.Id,
                Title = entity.Title,
            };
            return viewModel;
        }
        public static StatusEntity Map(StatusViewModel viewModel)// Метод для отображения представления StatusViewModel на сущность StatusEntity.
        {
            var entity = new StatusEntity// Создание объекта сущности и копирование данных из представления.
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
            };

            return entity;
        }
        public static List<StatusViewModel> Map(List<StatusEntity> entities)// Метод для отображения списка сущностей StatusEntity на список представлений StatusViewModel.
        {
            var viewModels = entities.Select(x => Map(x)).ToList();// Преобразование каждой сущности в соответствующее представление и создание списка представлений
            return viewModels;
        }
    }
}
