using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.Mappers
{
    public static class ProductCategoryMapper
    {
        public static ProductCategoryViewModel Map(ProductCategoryEntity entity)
        {
            if (entity == null)
                return null;
            var viewModel = new ProductCategoryViewModel
            {
                Id = entity.Id,
                Name = entity.Name,
                
            };
            return viewModel;
        }
        public static ProductCategoryEntity Map(ProductCategoryViewModel viewModel)
        {
            if (viewModel == null)
                return null;
            var entity = new ProductCategoryEntity
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                
            };
            return entity;
        }

        public static List<ProductCategoryViewModel> Map(List<ProductCategoryEntity> entities)
        {
            var viewModels = entities.Select(x => Map(x)).ToList();
            return viewModels;
        }
    }
}
