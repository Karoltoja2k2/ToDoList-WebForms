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

        int Delete(int entityId);
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

        public int Delete(int entityId)
        {
            var toDelete = Context.Set<T>().SingleOrDefault(x => x.Id == entityId);
            if (toDelete == null)
                return 0;
            
            Context.Set<T>().Remove(toDelete);
            Context.SaveChanges();
            return toDelete.Id;
        }

        public T Get(int id)
        {
            return Context.Set<T>().FirstOrDefault(x => x.Id == id);
        }
    }
}