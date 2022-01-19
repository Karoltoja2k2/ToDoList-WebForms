using System;
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

        object DataSource { get; set; }

        void RefreshData();

        void FormDataChanged(ToDoItem toDoItem, bool? isVisible = null);

        void ResultDisplayDataChanged();
    }
}