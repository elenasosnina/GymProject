using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.Mappers
{
    public static class LessonProgramMapper
    {
        public static LessonProgramViewModel Map(LessonProgramEntity entity)
        {
            if (entity == null)
                return null;
            var viewModel = new LessonProgramViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                ProgramDuration = entity.ProgramDuration,
                

            };
            return viewModel;
        }
        public static LessonProgramEntity Map(LessonProgramViewModel viewModel)
        {
            var entity = new LessonProgramEntity
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.Description,
                ProgramDuration = viewModel.ProgramDuration,
            };

            return entity;
        }

        public static List<LessonProgramViewModel> Map(List<LessonProgramEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
