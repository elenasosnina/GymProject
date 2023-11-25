using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class ProductCategoryRepository
    {
        public List<ProductCategoryEntity> GetList()
        {
            using (var context = new Context())
            {
                var items = context.ProductCategorys.ToList();
                return items;
            }
        }
        public ProductCategoryEntity GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.ProductCategorys.FirstOrDefault(x => x.ID == id);
                return item;
            }
        }

    }
}
