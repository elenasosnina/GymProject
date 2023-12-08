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
        public static StatusViewModel Map(StatusEntity entity)
        {
            if (entity == null)
                return null;
            var viewModel = new StatusViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
            };
            return viewModel;
        }
        public static StatusEntity Map(StatusViewModel viewModel)
        {
            var entity = new StatusEntity
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
            };

            return entity;
        }
        public static List<StatusViewModel> Map(List<StatusEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
