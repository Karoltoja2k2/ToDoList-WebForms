using System;

namespace ToDoList.View
{
    public interface IDefaultView
    {
        int FormId { get; set; }

        string FormTitle { get; set; }

        string FormDescription { get; set; }

        DateTime FormDueDate { get; set; }

        void UpdateResultDisplay();

        void TriggerFormVisibility(bool visible);
    }
}