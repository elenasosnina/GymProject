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
        public static DiscountViewModel Map(DiscountEntity entity)
        {
            if (entity == null)
                return null;

            var viewModel = new DiscountViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Value = entity.Value,
            };
            return viewModel;
        }
        public static DiscountEntity Map(DiscountViewModel viewModel)
        {
            if (viewModel == null)
                return null;

            var entity = new DiscountEntity
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Value = viewModel.Value,
            };
            return entity;
        }

        public static List<DiscountViewModel> Map(List<DiscountEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
