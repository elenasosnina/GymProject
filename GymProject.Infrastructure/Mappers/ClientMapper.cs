using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Data.Entity;
using GymProject.Infrastructure.ViewModels;


namespace GymProject.Infrastructure.Mappers
{
    public static class ClientMapper
    {
        public static ClientViewModel Map(ClientEntity entity)// Метод для отображения сущности ClientEntity на представление ClientViewModel.
        {
            if (entity == null) // Проверка наличия сущности.
                return null;
            var viewModel = new ClientViewModel// Создание объекта представления и копирование данных из сущности.
            {
                Id = entity.Id,
                Name = entity.Name,
                SecondName = entity.SecondName,
                MiddleName = entity.MiddleName,
                DateOfBirth = entity.DateOfBirth,
                Login = entity.Login,
                Password = entity.Password,
                DiscountId = entity.DiscountId,
                Discount = DiscountMapper.Map(entity.Discount),
            };
            return viewModel;
        }
        public static ClientEntity Map(ClientViewModel viewModel)  // Метод для отображения представления ClientViewModel на сущность ClientEntity.
        {
            var entity = new ClientEntity// Создание объекта сущности и копирование данных из представления.
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                SecondName = viewModel.SecondName,
                MiddleName = viewModel.MiddleName,
                DateOfBirth = viewModel.DateOfBirth,
                Login = viewModel.Login,
                Password = viewModel.Password,
                DiscountId = viewModel.DiscountId,
                Discount = DiscountMapper.Map(viewModel.Discount),
            };

            return entity;
        }

        public static List<ClientViewModel> Map(List<ClientEntity> entities)  // Метод для отображения списка сущностей ClientEntity на список представлений ClientViewModel.
        {
            var viewModels = entities.Select(x => Map(x)).ToList();// Преобразование каждой сущности в соответствующее представление и создание списка представлений
            return viewModels;
        }
    }

}
