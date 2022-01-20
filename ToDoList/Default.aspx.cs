using System;
using System.Web.UI;
using ToDoList.DataLayer.Model;
using ToDoList.DataLayer.Repository;

namespace ToDoList
{
    public partial class Default : Page
    {
        public const string QueryStringUserIdKey = "id";

        private IUserRepository _userRepository;

        protected void Page_Load(object sender, EventArgs e)
        {
            _userRepository = new UserRepository();
        }

        protected void LoginClick(object sender, EventArgs e)
        {
            var userName = UsernameTextBox.Text;
            var user = _userRepository.GetByName(userName);
            if (user == null)
            {
                UserExistValidator.IsValid = false;
                UserExistValidator.ErrorMessage = "User not found";
                return;
            }

            Master.UserName = user.Name;
            Response.Redirect($"ToDoItems.aspx?{QueryStringUserIdKey}={user.Id}");
        }
        
        protected void RegisterClick(object sender, EventArgs e)
        {
            var userName = UsernameTextBox.Text;
            var user = _userRepository.GetByName(userName);
            if (user != null)
            {
                UserExistValidator.IsValid = false;
                UserExistValidator.ErrorMessage = $"User already exist";
                return;
            }

            user = new User { Name = userName };
            user.Id = _userRepository.Add(user);
            Master.UserName = userName;
            Response.Redirect($"ToDoItems.aspx?{QueryStringUserIdKey}={user.Id}");
        }
    }
}