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
        public static SubscriptionTypeViewModel Map(SubscriptionTypeEntity entity)
        {
            if (entity == null)
                return null;
            var viewModel = new SubscriptionTypeViewModel
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
        public static SubscriptionTypeEntity Map(SubscriptionTypeViewModel viewModel)
        {
            var entity = new SubscriptionTypeEntity
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
        public static List<SubscriptionTypeViewModel> Map(List<SubscriptionTypeEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
