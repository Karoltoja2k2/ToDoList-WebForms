using ToDoList.DataLayer.Model;

namespace ToDoList.View
{
    public interface IDefaultView
    {
        string UserName { get; }

        void UserNotFound();

        void LoginSuccess(User user);

        void UserAlreadyExist();

        void RegisterSuccess(User user);
    }
}