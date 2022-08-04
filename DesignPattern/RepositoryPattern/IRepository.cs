using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesignPattern.RepositoryPattern
{
    public interface IRepository<TEntity>
    {//Esto es un GENERIC
        IEnumerable<TEntity> Get();
        TEntity Get(int id);
        void Add(TEntity beer);
        void Delete(int id);
        void Update(TEntity beer);

        void Save();
    }
}
