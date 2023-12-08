using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace GymProject.Infrastructure.Mappers
{
    public static class ProductMapper
    {
        public static ProductViewModel Map(ProductEntity entity)
        {
            if (entity == null)
                return null;
            var viewModel = new ProductViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                Cost = entity.Cost,
                Quantity = entity.Quantity,
                ExpirationDate = entity.ExpirationDate,
                ProductCategoryId = entity.ProductCategoryId,
                ProductCategory = ProductCategoryMapper.Map(entity.Product_category),
            };
            return viewModel;
        }

        public static ProductEntity Map(ProductViewModel viewModel)
        {
            var entity = new ProductEntity
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Cost = viewModel.Cost,
                Quantity = viewModel.Quantity,
                ExpirationDate = viewModel.ExpirationDate,
                ProductCategoryId = viewModel.ProductCategoryId,
                Product_category = ProductCategoryMapper.Map(viewModel.ProductCategory),
            };

            return entity;
        }

        public static List<ProductViewModel> Map(List<ProductEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
