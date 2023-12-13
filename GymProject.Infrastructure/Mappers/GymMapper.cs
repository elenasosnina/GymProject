using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.Mappers
{
    public static class GymMapper
    {
        public static GymViewModel Map(GymEntity entity)// Метод для отображения сущности GymEntity на представление GymViewModel.
        {
            if (entity == null) // Проверка наличия сущности.
                return null;
            var viewModel = new GymViewModel// Создание объекта представления и копирование данных из сущности.
            {
                Id = entity.Id,
                Name = entity.Name,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                Adress = entity.Adress,
            };
            return viewModel;
        }
        public static GymEntity Map(GymViewModel viewModel)// Метод для отображения представления GymViewModel на сущность GymEntity.
        {
            var entity = new GymEntity// Создание объекта сущности и копирование данных из представления.
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                StartTime = viewModel.StartTime,
                EndTime = viewModel.EndTime,
                Adress = viewModel.Adress,
            };

            return entity;
        }

        public static List<GymViewModel> Map(List<GymEntity> entities)// Метод для отображения списка сущностей GymEntity на список представлений GymViewModel.
        {
            var viewModels = entities.Select(x => Map(x)).ToList();// Преобразование каждой сущности в соответствующее представление и создание списка представлений
            return viewModels;
        }
    }
}
