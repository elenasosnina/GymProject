using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class StatusRepository
    {
        public StatusViewModel Update(StatusEntity entity)
        {
            //entity.Name = entity.Name.Trim();
            //if (string.IsNullOrEmpty(entity.Name))
            //{
            //    throw new Exception("Имя пользователя не может быть пустым");
            //}
            using (var context = new Context())
            {
                var existingClient = context.Statuses.Find(entity.Id);

                if (existingClient != null)
                {
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return StatusMapper.Map(entity);
        }
        public StatusViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var clientToRemove = context.Statuses.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.Statuses.Remove(clientToRemove);
                    context.SaveChanges();
                }
                return StatusMapper.Map(clientToRemove);
            }
        }
        public StatusViewModel Add(StatusEntity entity)
        {
            using (var context = new Context())
            {
                context.Statuses.Add(entity);
                context.SaveChanges();
            }
            return StatusMapper.Map(entity);
        }
        public List<StatusViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Statuses.ToList();
                return StatusMapper.Map(items);
            }
        }
        public StatusViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Statuses.FirstOrDefault(x => x.Id == id);
                return StatusMapper.Map(item);
            }
        }

    }
}
