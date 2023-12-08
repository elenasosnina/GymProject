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
        public static GymViewModel Map(GymEntity entity)
        {
            if (entity == null)
                return null;
            var viewModel = new GymViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                StartTime = entity.StartTime,
                EndTime = entity.EndTime,
                Adress = entity.Adress,
            };
            return viewModel;
        }
        public static GymEntity Map(GymViewModel viewModel)
        {
            var entity = new GymEntity
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                StartTime = viewModel.StartTime,
                EndTime = viewModel.EndTime,
                Adress = viewModel.Adress,
            };

            return entity;
        }

        public static List<GymViewModel> Map(List<GymEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
