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
        public static EmployeeViewModel Map(EmployeeEntity entity)// Метод для отображения сущности EmployeeEntity на представление EmployeeViewModel.
        {
            if (entity == null)// Проверка наличия сущности.
                return null;
            var viewModel = new EmployeeViewModel// Создание объекта представления и копирование данных из сущности.
            {
                Id = entity.Id,
                Name = entity.Name,
                SurName = entity.SurName,
                MiddleName = entity.MiddleName,
                DateOfBirth = entity.DateOfBirth,
                Gender = entity.Gender,
                Login = entity.Login,
                LengthOfService = entity.LengthOfService,
                Password = entity.Password,
                PositionId = entity.PositionId,
                Position = PositionMapper.Map(entity.Position),
            };
            return viewModel;
        }
        public static EmployeeEntity Map(EmployeeViewModel viewModel)// Метод для отображения представления EmployeeViewModel на сущность EmployeeEntity.
        {
            var entity = new EmployeeEntity// Создание объекта сущности и копирование данных из представления.
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                SurName = viewModel.SurName,
                MiddleName = viewModel.MiddleName,
                DateOfBirth = viewModel.DateOfBirth,
                Gender = viewModel.Gender,
                Login = viewModel.Login,
                LengthOfService = viewModel.LengthOfService,
                Password = viewModel.Password,
                PositionId = viewModel.PositionId,
                Position = PositionMapper.Map(viewModel.Position)
            };

            return entity;
        }

        public static List<EmployeeViewModel> Map(List<EmployeeEntity> entities)// Метод для отображения списка сущностей EmployeeEntity на список представлений EmployeeViewModel.
        {
            var viewModels = entities.Select(x => Map(x)).ToList();// Преобразование каждой сущности в соответствующее представление и создание списка представлений
            return viewModels;
        }
    }
}
