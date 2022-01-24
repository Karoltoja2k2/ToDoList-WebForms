using System;
using System.Web;
using System.Web.UI;
using ToDoList.DataLayer.Model;
using ToDoList.Helper;
using ToDoList.Presenter;
using ToDoList.View;

namespace ToDoList
{
    public partial class Default : Page, IDefaultView
    {
        public const string QueryStringUserIdKey = "id";

        private DefaultViewPresenter _presenter;

        public string UserName 
        {
            get => UsernameTextBox.Text;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new DefaultViewPresenter(this);
            if (!IsPostBack)
            {
                LoadComplete += (s, ev) => Page.DataBind();
            }
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
            HttpContext.Current.Session[AppConst.UserIdSessionKey] = user.Id;
            HttpContext.Current.Session[AppConst.UserNameSessionKey] = user.Name;
            Response.Redirect($"ToDoItems.aspx");
        }
    }
}