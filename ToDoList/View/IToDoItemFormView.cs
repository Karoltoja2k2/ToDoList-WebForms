using System;

namespace ToDoList.View
{
    public interface IToDoItemFormView
    {
        int FormId { get; set; }

        string FormTitle { get; set; }

        string FormDescription { get; set; }

        DateTime FormDueDate { get; set; }

        void RefreshWraper();
    }
}