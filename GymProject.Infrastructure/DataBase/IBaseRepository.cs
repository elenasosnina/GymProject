using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    internal interface IBaseRepository<TEntity>// Интерфейс базового репозитория для работы с сущностями.
    {
        TEntity GetById(long id);            // Получение сущности по идентификатору.
        List<TEntity> GetList();             // Получение списка всех сущностей.
        TEntity Update(TEntity entity);      // Обновление данных сущности.
        TEntity Delete(long id);             // Удаление сущности по идентификатору.
        TEntity Add(TEntity entity);          // Добавление новой сущности.
        List<TEntity> Search(string search);  // Поиск сущностей по строке поиска.

    }

}
