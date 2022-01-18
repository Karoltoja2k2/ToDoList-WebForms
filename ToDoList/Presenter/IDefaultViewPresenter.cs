namespace ToDoList.Presenter
{
    public interface IDefaultViewPresenter
    {
        void SubmitForm();

        void UpdateIsDone(int toDoItemId, bool isDone);

        void DeleteToDoItem(int toDoItemId);

        void ClearForm();

        void SetFormData(int toDoItemId);

        bool OnRowCommand(int itemId, string commmandName);
    }
}