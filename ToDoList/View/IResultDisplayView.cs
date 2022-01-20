using System;
using System.Collections.Generic;
using ToDoList.DataLayer.Model;

namespace ToDoList.View
{
    public interface IResultDisplayView
    {
        int UserId { get; }

        string IsDone { get; }

        string Title { get; }

        string Description { get; }

        DateTime DueDateFrom { get; }

        DateTime DueDateTo { get; }

        int Total { get; set; }

        int Amount { get; }

        int CurrentPage { get; }

        void SetDataSource(List<ToDoItem> data);

        void FormDataChanged(ToDoItem toDoItem, bool? isVisible = null);

        void ResultDisplayDataChanged();
    }
}