using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.Mappers
{
    public static class SubscriptionTypeMapper
    {
        public static SubscriptionTypeViewModel Map(SubscriptionTypeEntity entity)// Метод для отображения сущности SubscriptionTypeEntity на представление SubscriptionTypeViewModel.
        {
            if (entity == null)// Проверка наличия сущности.
                return null;
            var viewModel = new SubscriptionTypeViewModel// Создание объекта представления и копирование данных из сущности.
            {
                Id = entity.Id,
                Name = entity.Name,
                Duration = entity.Duration,
                NumberOfClasses = entity.NumberOfClasses,
                DateAndTimeOfPurchase = entity.DateAndTimeOfPurchase,
                Cost = entity.Cost,
            };
            return viewModel;
        }
        public static SubscriptionTypeEntity Map(SubscriptionTypeViewModel viewModel)// Метод для отображения представления SubscriptionTypeViewModel на сущность SubscriptionTypeEntity.
        {
            var entity = new SubscriptionTypeEntity// Создание объекта сущности и копирование данных из представления.
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Duration = viewModel.Duration,
                NumberOfClasses = viewModel.NumberOfClasses,
                DateAndTimeOfPurchase = viewModel.DateAndTimeOfPurchase,
                Cost = viewModel.Cost,
            };

            return entity;
        }
        public static List<SubscriptionTypeViewModel> Map(List<SubscriptionTypeEntity> entities)// Метод для отображения списка сущностей SubscriptionTypeEntity на список представлений SubscriptionTypeViewModel.
        {
            var viewModels = entities.Select(x => Map(x)).ToList();// Преобразование каждой сущности в соответствующее представление и создание списка представлений
            return viewModels;
        }
    }
}
