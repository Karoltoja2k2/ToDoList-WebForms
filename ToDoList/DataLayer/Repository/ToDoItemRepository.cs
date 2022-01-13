﻿using System;
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

        List<ToDoItem> Get(int isDone, string title, string description);

        ToDoItem GetById(int id);

        void Delete(int id);

        void Update(ToDoItem item);
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
            return _context.ToDoItem.OrderBy(x => x.DueDate).ToList();
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

        public ToDoItem GetById(int id)
        {
            return _context.ToDoItem.FirstOrDefault(x => x.Id == id);
        }

        public List<ToDoItem> Get(int isDone, string title, string description)
        {
            bool? filter = null;
            switch (isDone)
            {
                case 1: filter = true; break;
                case 2: filter = false; break;  
            }
            
            var query = _context.ToDoItem.AsQueryable();
            if (filter.HasValue)
                query = query.Where(x => x.IsDone == filter.Value);

            if (!string.IsNullOrWhiteSpace(title))
                query = query.Where(x => x.Title.Contains(title));

            if (!string.IsNullOrWhiteSpace(description))
                query = query.Where(x => x.Description.Contains(description));

            return query.OrderBy(x => x.DueDate).ToList();
        }
    }
}