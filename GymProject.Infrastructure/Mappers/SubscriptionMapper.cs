using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.Mappers
{
    public static class SubscriptionMapper
    {
        public static SubscriptionViewModel Map(SubscriptionEntity entity)
        {
            if (entity == null)
                return null;
            var viewModel = new SubscriptionViewModel
            {
                Id = entity.Id,
                ValidityStartDate = entity.ValidityStartDate,
                ValidityExpirationDate = entity.ValidityExpirationDate,
                Client = ClientMapper.Map(entity.Client),
                ClientId = entity.ClientId,
                Status = StatusMapper.Map(entity.Status),
                StatusId = entity.StatusId,
                Subscription_type = SubscriptionTypeMapper.Map(entity.Subscription_type),
                SubscriptionTypeId = entity.SubscriptionTypeId,
            };
            return viewModel;
        }
        public static SubscriptionEntity Map(SubscriptionViewModel viewModel)
        {
            var entity = new SubscriptionEntity
            {
                Id = viewModel.Id,
                ValidityStartDate = viewModel.ValidityStartDate,
                ValidityExpirationDate = viewModel.ValidityExpirationDate,
                Client = ClientMapper.Map(viewModel.Client),
                ClientId = viewModel.ClientId,
                Status = StatusMapper.Map(viewModel.Status),
                StatusId = viewModel.StatusId,
                Subscription_type = SubscriptionTypeMapper.Map(viewModel.Subscription_type),
                SubscriptionTypeId = viewModel.SubscriptionTypeId,
            };
            return entity;
        }

        public static List<SubscriptionViewModel> Map(List<SubscriptionEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
