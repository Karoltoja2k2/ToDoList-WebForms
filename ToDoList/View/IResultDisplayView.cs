using System;
using System.Collections.Generic;
using ToDoList.DataLayer.Model;

namespace ToDoList.View
{
    public interface IResultDisplayView
    {
        string IsDone { get; set; }

        string Title { get; set; }

        string Description { get; set; }

        DateTime DueDateFrom { get; set; }

        DateTime DueDateTo { get; set; }

        void SetDataSource(List<ToDoItem> data);

        void FormDataChanged(ToDoItem toDoItem, bool? isVisible = null);

        void ResultDisplayDataChanged();
    }
}