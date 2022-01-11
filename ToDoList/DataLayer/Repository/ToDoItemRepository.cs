using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ToDoList.DataLayer.Model;

namespace ToDoList.DataLayer.Repository
{
    public class ToDoItemRepository
    {
        private readonly ToDoListDbContext _context = new ToDoListDbContext();

        public int Add(ToDoItem item)
        {
            var id = _context.ToDoItem.Add(item).Id;
            _context.SaveChanges();
            return id;
        }

        public List<ToDoItem> Get()
        {
            return _context.ToDoItem.ToList();
        }
    }
}