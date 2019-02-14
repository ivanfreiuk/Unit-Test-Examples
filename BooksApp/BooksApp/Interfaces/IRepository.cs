using System.Collections.Generic;

namespace BooksApp.Interfaces
{
    public interface IRepository<TEntity>
    {
        TEntity Get(int id);
        IEnumerable<TEntity> GetAll();

        void Add(TEntity entity);
        void Remove(TEntity entity);
        void Update(TEntity entity);
    }
}
