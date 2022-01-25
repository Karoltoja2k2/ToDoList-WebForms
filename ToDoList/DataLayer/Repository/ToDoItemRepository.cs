using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.DataLayer.Model;

namespace ToDoList.DataLayer.Repository
{
    public interface IToDoItemRepository: IRepositoryBase<ToDoItem>
    {
        void UpdateIsDone(int id, bool isDone);

        (int total, List<ToDoItem> items) Get(int userId, 
            int isDone,
            string title,
            string description,
            DateTime? dueDateFrom,
            DateTime? dueDateTo,
            int currentPage,
            int amount);

        void Update(ToDoItem item);
    }

    public class ToDoItemRepository : RepositoryBase<ToDoItem>,  IToDoItemRepository
    {
        public ToDoItemRepository()
            : base (new ToDoListDbContext())
        {
        }

        private ToDoListDbContext _context { get => (ToDoListDbContext)Context; }

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

        public (int total, List<ToDoItem> items) Get(int userId,
            int isDone,
            string title,
            string description,
            DateTime? dueDateFrom,
            DateTime? dueDateTo,
            int currentPage,
            int amount)
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

            var total = query.Count();
            var items = query.OrderBy(x => x.DueDate)
                .Skip(amount * (currentPage - 1))
                .Take(amount)
                .ToList();

            return (total, items);
        }
    }
}