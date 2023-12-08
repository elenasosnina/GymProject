﻿using GymProject.Infrastructure.Mappers;
using GymProject.Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    public class LessonProgramRepository
    {
        public LessonProgramViewModel Update(LessonProgramEntity entity)
        {
            //entity.Name = entity.Name.Trim();
            //if (string.IsNullOrEmpty(entity.Name))
            //{
            //    throw new Exception("Имя пользователя не может быть пустым");
            //}
            using (var context = new Context())
            {
                var existingClient = context.LessonPrograms.Find(entity.Id);

                if (existingClient != null)
                {
                    context.Entry(existingClient).CurrentValues.SetValues(entity);
                    context.SaveChanges();
                }
            }
            return LessonProgramMapper.Map(entity);
        }
        public LessonProgramViewModel Delete(long id)
        {
            using (var context = new Context())
            {
                var clientToRemove = context.LessonPrograms.FirstOrDefault(c => c.Id == id);
                if (clientToRemove != null)
                {
                    context.LessonPrograms.Remove(clientToRemove);
                    context.SaveChanges();
                }
                return LessonProgramMapper.Map(clientToRemove);
            }
        }
        public LessonProgramViewModel Add(LessonProgramEntity entity)
        {
            using (var context = new Context())
            {
                context.LessonPrograms.Add(entity);
                context.SaveChanges();
            }
            return LessonProgramMapper.Map(entity);
        }
        public List<LessonProgramViewModel> GetList()
        {
            using (var context = new Context())
            {
                var items = context.LessonPrograms.ToList();
                return LessonProgramMapper.Map(items);
            }
        }
        public LessonProgramViewModel GetById(long id)
        {
            using (var context = new Context())
            {
                var item = context.LessonPrograms.FirstOrDefault(x => x.Id == id);
                return LessonProgramMapper.Map(item);
            }
        }

    }
}
