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
        public static ClientViewModel Map(ClientEntity entity)
        {
            if (entity == null)
                return null;
            var viewModel = new ClientViewModel
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
        public static ClientEntity Map(ClientViewModel viewModel)
        {
            var entity = new ClientEntity
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

        public static List<ClientViewModel> Map(List<ClientEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }

}
