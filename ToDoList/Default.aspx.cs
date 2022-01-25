using System;
using System.Web.UI;
using ToDoList.DataLayer.Model;
using ToDoList.Presenter;
using ToDoList.View;

namespace ToDoList
{
    public partial class Default : Page, IDefaultView
    {
        public const string QueryStringUserIdKey = "id";

        private readonly IDefaultViewPresenter _presenter;

        public Default(IDefaultViewPresenter presenter)
        {
            _presenter = presenter;
            _presenter.SetView(this);
        }

        public string UserName 
        {
            get => UsernameTextBox.Text;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter.SetView(this);
        }

        protected void LoginClick(object sender, EventArgs e) =>
            _presenter.OnLogin();

        public void LoginSuccess(User user) =>
            OnUserSelected(user);

        public void UserNotFound() =>
            UserExistValidatorFailed("User not found");

        protected void RegisterClick(object sender, EventArgs e) =>
            _presenter.OnRegisterClick();

        public void RegisterSuccess(User user) =>
            OnUserSelected(user);

        public void UserAlreadyExist() =>
            UserExistValidatorFailed("User already exists");

        private void UserExistValidatorFailed(string message)
        {
            UserExistValidator.IsValid = false;
            UserExistValidator.ErrorMessage = message;
        }

        private void OnUserSelected(User user)
        {
            Master.UserName = user.Name;
            Response.Redirect($"ToDoItems.aspx?{QueryStringUserIdKey}={user.Id}");
        }
    }
}