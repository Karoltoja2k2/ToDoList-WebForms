namespace ToDoList.Presenter
{
    public interface IPresenterBase<T>
    {
        void SetView(T view);
    }
}
