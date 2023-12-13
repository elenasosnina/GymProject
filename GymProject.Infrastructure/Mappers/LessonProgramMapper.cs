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
        public static LessonProgramViewModel Map(LessonProgramEntity entity)// Метод для отображения сущности LessonProgramEntity на представление LessonProgramViewModel.
        {
            if (entity == null) // Проверка наличия сущности.
                return null;
            var viewModel = new LessonProgramViewModel// Создание объекта представления и копирование данных из сущности.
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                ProgramDuration = entity.ProgramDuration,
                

            };
            return viewModel;
        }
        public static LessonProgramEntity Map(LessonProgramViewModel viewModel)// Метод для отображения представления LessonProgramViewModel на сущность LessonProgramEntity.
        {
            var entity = new LessonProgramEntity// Создание объекта сущности и копирование данных из представления.
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Description = viewModel.Description,
                ProgramDuration = viewModel.ProgramDuration,
            };

            return entity;
        }

        public static List<LessonProgramViewModel> Map(List<LessonProgramEntity> entities)// Метод для отображения списка сущностей LessonProgramEntity на список представлений LessonProgramViewModel.
        {
            var viewModels = entities.Select(x => Map(x)).ToList();// Преобразование каждой сущности в соответствующее представление и создание списка представлений
            return viewModels;
        }
    }
}
