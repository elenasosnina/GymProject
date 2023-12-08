using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class GymRepository
    {
        public GymViewModel Update(GymEntity entity)
        {
            entity.Name = entity.Name.Trim();
            if (string.IsNullOrEmpty(entity.Name))
            {
                throw new Exception("Имя пользователя не может быть пустым");
            }
            using (var context = new Context())
            {
                var existingClient = context.Gyms.Find(entity.Id);

                if (existingClient != null)
                {
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return GymMapper.Map(entity);
        }
        public GymViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var clientToRemove = context.Gyms.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.Gyms.Remove(clientToRemove);
                    context.SaveChanges();
                }
                return GymMapper.Map(clientToRemove);
            }
        }
        public GymViewModel Add(GymEntity entity)
        {
            using (var context = new Context())
            {
                context.Gyms.Add(entity);
                context.SaveChanges();
            }
            return GymMapper.Map(entity);
        }
        public List<GymViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Gyms.ToList();
                return GymMapper.Map(items);
            }
        }
        public GymViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Gyms.FirstOrDefault(x => x.Id == id);
                return GymMapper.Map(item);
            }
        }

    }
}
