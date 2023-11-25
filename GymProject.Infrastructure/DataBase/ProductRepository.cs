using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class ProductRepository
    {
        public List<ProductEntity> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Products.ToList();
                return items;
            }
        }
        public ProductEntity GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Products.FirstOrDefault(x => x.Id == id);
                return item;
            }
        }

    }
}
