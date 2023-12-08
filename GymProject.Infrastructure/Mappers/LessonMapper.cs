using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GymProject.Infrastructure.Mappers
{
    public static class LessonMapper
    {
        public static LessonViewModel Map(LessonEntity entity)
        {
            if (entity == null)
                return null;
            var viewModel = new LessonViewModel
            {
                Id = entity.Id,
                DateAndTime = entity.DateAndTime,
                Hall = HallMapper.Map(entity.Hall),
                HallId = entity.HallId,
                Gym = GymMapper.Map(entity.Gym),
                GymId = entity.GymId,
                Subscription = SubscriptionMapper.Map(entity.Subscription),
                SubscriptionId = entity.SubscriptionId,
                Lesson_program = LessonProgramMapper.Map(entity.Lesson_programs),
                ProgramId = entity.ProgramId,

            };
            return viewModel;
        }
        public static LessonEntity Map(LessonViewModel viewModel)
        {
            var entity = new LessonEntity
            {
                Id = viewModel.Id,
                DateAndTime = viewModel.DateAndTime,
                Hall = HallMapper.Map(viewModel.Hall),
                HallId = viewModel.HallId,
                Gym = GymMapper.Map(viewModel.Gym),
                GymId = viewModel.GymId,
                Subscription = SubscriptionMapper.Map(viewModel.Subscription),
                SubscriptionId = viewModel.SubscriptionId,
                Lesson_programs = LessonProgramMapper.Map(viewModel.Lesson_program),
                ProgramId = viewModel.ProgramId,
            };
            return entity;
        }

        public static List<LessonViewModel> Map(List<LessonEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
