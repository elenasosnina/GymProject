using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class PositionRepository
    {
        public PositionViewModel Update(PositionEntity entity)
        {
            //entity = entity.Name.Trim();
            //if (string.IsNullOrEmpty(entity.Name))
            //{
            //    throw new Exception("Имя пользователя не может быть пустым");
            //}
            using (var context = new Context())
            {
                var existingClient = context.Positions.Find(entity.Id);

                if (existingClient != null)
                {
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return PositionMapper.Map(entity);
        }
        public PositionViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var clientToRemove = context.Positions.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.Positions.Remove(clientToRemove);
                    context.SaveChanges();
                }
                return PositionMapper.Map(clientToRemove);
            }
        }
        public PositionViewModel Add(PositionEntity entity)
        {
            using (var context = new Context())
            {
                context.Positions.Add(entity);
                context.SaveChanges();
            }
            return PositionMapper.Map(entity);
        }
        public List<PositionViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Positions.ToList();
                return PositionMapper.Map(items);
            }
        }
        public PositionViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Positions.FirstOrDefault(x => x.Id == id);
                return PositionMapper.Map(item);
            }
        }

    }
}
