using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Infrastructure.DataBase
{
    internal interface IBaseRepository<TEntity>
    {
        TEntity GetById(long id);
        List<TEntity> GetList();
    }

}
