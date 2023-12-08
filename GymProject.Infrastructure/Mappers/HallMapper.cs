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
        public static HallViewModel Map(HallEntity entity)
        {
            if (entity == null)
                return null;
            var viewModel = new HallViewModel
            {
                Id = entity.Id,
                HallNumber = entity.HallNumber,
            };
            return viewModel;
        }
        public static HallEntity Map(HallViewModel viewModel)
        {
            var entity = new HallEntity
            {
                Id = viewModel.Id,
                HallNumber = viewModel.HallNumber,
            };

            return entity;
        }
        public static List<HallViewModel> Map(List<HallEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
