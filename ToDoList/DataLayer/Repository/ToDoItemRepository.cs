using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.DataLayer.Model;

namespace ToDoList.DataLayer.Repository
{
    public interface IToDoItemRepository: IRepositoryBase<ToDoItem>
    {
        int Add(ToDoItem item);

        void UpdateIsDone(int id, bool isDone);

        List<ToDoItem> Get(int userId, 
            int isDone,
            string title,
            string description,
            DateTime? dueDateFrom,
            DateTime? dueDateTo);

        void Delete(int id);

        void Update(ToDoItem item);
    }

    public class ToDoItemRepository : RepositoryBase<ToDoItem>,  IToDoItemRepository
    {
        public ToDoItemRepository()
            : base (new ToDoListDbContext())
        {
        }

        private ToDoListDbContext _context { get => (ToDoListDbContext)Context; }

        public void Delete(int id)
        {
            var toDelete = _context.ToDoItem.SingleOrDefault(x => x.Id == id);
            if (toDelete != null)
            {
                _context.ToDoItem.Remove(toDelete);
                _context.SaveChanges();
            }
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

        public void Update(ToDoItem item)
        {
            var toUpdate = _context.ToDoItem.FirstOrDefault(x => x.Id == item.Id);
            if (toUpdate == null) return;
            toUpdate.Title = item.Title;
            toUpdate.Description = item.Description;
            toUpdate.DueDate = item.DueDate;
            _context.SaveChanges();
        }

        public List<ToDoItem> Get(int userId,
            int isDone,
            string title,
            string description,
            DateTime? dueDateFrom,
            DateTime? dueDateTo)
        {
            bool? filter = null;
            switch (isDone)
            {
                case 1: filter = true; break;
                case 2: filter = false; break;  
            }
            
            var query = _context
                .ToDoItem
                .AsQueryable()
                .Where(x => x.AssignedTo == userId);

            if (filter.HasValue)
                query = query.Where(x => x.IsDone == filter.Value);

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(x => x.Title.Contains(title));
            if (!string.IsNullOrWhiteSpace(description))
                query = query.Where(x => x.Description.Contains(description));

            if (dueDateFrom != null)
                query = query.Where(x => x.DueDate >= dueDateFrom.Value);
            if (dueDateTo != null)
                query = query.Where(x => x.DueDate <= dueDateTo.Value);

            return query.OrderBy(x => x.DueDate).ToList();
        }
    }
}