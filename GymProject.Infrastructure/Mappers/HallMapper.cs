using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.Mappers
{
    public static class HallMapper
    {
        public static HallViewModel Map(HallEntity entity) // Метод для отображения сущности HallEntity на представление HallViewModel.
        {
            if (entity == null) // Проверка наличия сущности.
                return null;
            var viewModel = new HallViewModel// Создание объекта представления и копирование данных из сущности.
            {
                Id = entity.Id,
                HallNumber = entity.HallNumber,
            };
            return viewModel;
        }
        public static HallEntity Map(HallViewModel viewModel)// Метод для отображения представления HallViewModel на сущность HallEntity.
        {
            var entity = new HallEntity// Создание объекта сущности и копирование данных из представления.
            {
                Id = viewModel.Id,
                HallNumber = viewModel.HallNumber,
            };

            return entity;
        }
        public static List<HallViewModel> Map(List<HallEntity> entities)// Метод для отображения списка сущностей HallEntity на список представлений HallViewModel.
        {
            var viewModels = entities.Select(x => Map(x)).ToList();// Преобразование каждой сущности в соответствующее представление и создание списка представлений
            return viewModels;
        }
    }
}
