using ToDoList.DataLayer.Model;

namespace ToDoList.View
{
    public interface IResultDisplayView
    {
        void FormDataChanged(ToDoItem toDoItem, bool isVisible);

        void ResultDisplayDataChanged();
    }
}