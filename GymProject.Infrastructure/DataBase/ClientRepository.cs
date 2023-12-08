﻿using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class ClientRepository
    {

        public List<ClientViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.Clients.Include(x => x.Discount).ToList();
                return ClientMapper.Map(items);
            }
        }
        public ClientViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.Clients.FirstOrDefault(x => x.Id == id);

                return ClientMapper.Map(item);
            }

        }
        public ClientViewModel Update(ClientEntity entity)
        { 
            entity.Name = entity.Name.Trim();
            if (string.IsNullOrEmpty(entity.Name))
            {
                throw new Exception("Имя пользователя не может быть пустым");
            }
            using (var context = new Context())
            {
                var existingClient = context.Clients.Find(entity.Id);

                if (existingClient != null)
                {
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return ClientMapper.Map(entity);
        }
        public ClientViewModel Delete(long id)
        {
            using (var context = new Context()) 
            {
                var clientToRemove = context.Clients.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null) {
                    context.Clients.Remove(clientToRemove);
                    context.SaveChanges();}
                return ClientMapper.Map(clientToRemove);
            }   
        }
        public ClientViewModel Add(ClientEntity entity)
        {
            using (var context = new Context()){
                context.Clients.Add(entity);
                context.SaveChanges();
            } 
            return ClientMapper.Map(entity);
        }
        public List<ClientEntity> Search(string search)
        {
            search = search.Trim();

            using (var context = new Context())
            { 
                var result = context.Clients.Include(c => c.Discount).Where(x => x.Name.Contains(search)).ToList();

                return result;
            }

        }
    }
}
