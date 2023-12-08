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
        public static PositionViewModel Map(PositionEntity entity)
        {
            if (entity == null)
                return null;
            var viewModel = new PositionViewModel
            {
                Id = entity.Id,
                Title = entity.Title,
                Salary = entity.Salary,
                WorkSchedule = entity.WorkSchedule,


            };
            return viewModel;
        }
        public static PositionEntity Map(PositionViewModel viewModel)
        {
            if (viewModel == null)
                return null;
            var entity = new PositionEntity
            {
                Id = viewModel.Id,
                Title = viewModel.Title,
                Salary = viewModel.Salary,
                WorkSchedule = viewModel.WorkSchedule,
            };

            return entity;
        }
        public static List<PositionViewModel> Map(List<PositionEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
