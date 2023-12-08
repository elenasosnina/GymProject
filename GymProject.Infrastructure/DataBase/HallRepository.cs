using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class HallRepository
    {
        public HallViewModel Update(HallEntity entity)
        {
            //entity.Name = entity.Name.Trim();
            //if (string.IsNullOrEmpty(entity.Name))
            //{
            //    throw new Exception("Имя пользователя не может быть пустым");
            //}
            using (var context = new Context())
            {
                var existingClient = context.Halls.Find(entity.Id);

                if (existingClient != null)
                {
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return HallMapper.Map(entity);
        }
        public HallViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var clientToRemove = context.Halls.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.Halls.Remove(clientToRemove);
                    context.SaveChanges();
                }
                return HallMapper.Map(clientToRemove);
            }
        }
        public HallViewModel Add(HallEntity entity)
        {
            using (var context = new Context())
            {
                context.Halls.Add(entity);
                context.SaveChanges();
            }
            return HallMapper.Map(entity);
        }
        public List<HallViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Halls.ToList();
                return HallMapper.Map(items);
            }
        }
        public HallViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Halls.FirstOrDefault(x => x.Id == id);
                return HallMapper.Map(item);
            }
        }
    }
}
