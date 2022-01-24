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

        void OnRowDelete();

        void SetDataSource(List<ToDoItem> data);

        void SetFormData(int toDoItemId);
    }
}