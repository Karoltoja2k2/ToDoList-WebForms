using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.DataLayer.Model;

namespace ToDoList.DataLayer.Repository
{
    public interface IToDoItemRepository
    {
        int Add(ToDoItem item);

        void UpdateIsDone(int id, bool isDone);

        List<ToDoItem> Get();

        void Delete(int id);
    }

    public class ToDoItemRepository : IToDoItemRepository
    {
        private readonly ToDoListDbContext _context = new ToDoListDbContext();

        public int Add(ToDoItem item)
        {
            var id = _context.ToDoItem.Add(item).Id;
            _context.SaveChanges();
            return id;
        }

        public void Delete(int id)
        {
            var toDelete = _context.ToDoItem.SingleOrDefault(x => x.Id == id);
            if (toDelete != null)
            {
                _context.ToDoItem.Remove(toDelete);
                _context.SaveChanges();
            }
        }

        public List<ToDoItem> Get()
        {
            return _context.ToDoItem.ToList();
        }

        public void UpdateIsDone(int id, bool isDone)
        {
            var result = _context.ToDoItem.SingleOrDefault(x => x.Id == id);
            if (result != null)
            {
                result.IsDone = isDone;
                _context.SaveChanges();
            }
        }
    }
}