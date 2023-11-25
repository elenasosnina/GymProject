using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class ClientRepository
    {
        public List<ClientEntity> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Clients.ToList();
                return items;
            }
        }
        public ClientEntity GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Clients.FirstOrDefault(x => x.ID == id);
                return item;
            }
        }

    }
}
