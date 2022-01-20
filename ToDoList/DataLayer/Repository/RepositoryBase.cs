using System.Data.Entity;
using System.Linq;
using ToDoList.DataLayer.Model.Base;

namespace ToDoList.DataLayer.Repository
{
    public interface IRepositoryBase<T>
        where T : BaseEntity
    {
        T Get(int id);

        int Add(T entity);

        int Delete(T entity);
    }

    public class RepositoryBase<T> : IRepositoryBase<T>
        where T : BaseEntity
    {
        protected readonly DbContext Context;

        public RepositoryBase(DbContext context)
        {
            Context = context;
        }

        public int Add(T entity)
        {
            Context.Set<T>().Add(entity);
            Context.SaveChanges();
            return entity.Id;
        }

        public int Delete(T entity)
        {
            Context.Set<T>().Remove(entity);
            Context.SaveChanges();
            return entity.Id;
        }

        public T Get(int id)
        {
            return Context.Set<T>().FirstOrDefault(x => x.Id == id);
        }
    }
}