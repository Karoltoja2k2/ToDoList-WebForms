using System;
using ToDoList.DataLayer.Model.Base;

namespace ToDoList.DataLayer.Model
{
    [Serializable]
    public class ToDoItem : BaseEntity
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public int Status { get; set; }

        public bool IsDone { get; set; }

        public DateTime DueDate { get; set; }

        public int CreatedBy { get; set; }
    }
}