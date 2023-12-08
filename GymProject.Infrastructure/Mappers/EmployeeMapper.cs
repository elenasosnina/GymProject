using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;


namespace GymProject.Infrastructure.Mappers
{
    public static class EmployeeMapper
    {
        public static EmployeeViewModel Map(EmployeeEntity entity)
        {
            if (entity == null)
                return null;
            var viewModel = new EmployeeViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                SurName = entity.SurName,
                MiddleName = entity.MiddleName,
                DateOfBirth = entity.DateOfBirth,
                Gender = entity.Gender == "м" ? "мужской" : "женский",
                Login = entity.Login,
                LengthOfService = entity.LengthOfService,
                Password = entity.Password,
                PositionId = entity.PositionId,
                Position = PositionMapper.Map(entity.Position),
            };
            return viewModel;
        }
        public static EmployeeEntity Map(EmployeeViewModel viewModel)
        {
            var entity = new EmployeeEntity
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                SurName = viewModel.SurName,
                MiddleName = viewModel.MiddleName,
                DateOfBirth = viewModel.DateOfBirth,
                Gender = viewModel.Gender == "м" ? "мужской" : "женский",
                Login = viewModel.Login,
                LengthOfService = viewModel.LengthOfService,
                Password = viewModel.Password,
                PositionId = viewModel.PositionId,
                Position = PositionMapper.Map(viewModel.Position)
            };

            return entity;
        }

        public static List<EmployeeViewModel> Map(List<EmployeeEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
