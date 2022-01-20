using System;
using System.Web.UI;
using ToDoList.DataLayer.Model;
using ToDoList.DataLayer.Repository;

namespace ToDoList
{
    public delegate void ResultDisplayDataChangedHandler();

    public delegate void ToDoItemFormDataChangedHandler(ToDoItem item, bool? visible);

    public partial class ToDoItems : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var _userRepository = new UserRepository();

            if (!IsPostBack)
            {
                var userId = int.Parse(Request.QueryString[Default.QueryStringUserIdKey]);
                ResultDisplay.SetUserId(userId);
                ToDoItemForm.SetUserId(userId);
                Master.UserName = _userRepository.Get(userId).Name;
                Page.DataBind();
            }

            ToDoItemForm.ResultDisplayDataChangedEvent += OnResultDisplayDataChanged;
            ResultDisplay.ToDoItemFormDataChangedEvent += OnFormDataChaned;
            ResultDisplay.ResultDisplayDataChangedEvent += OnResultDisplayDataChanged;
        }

        private void OnResultDisplayDataChanged() => ResultDisplay.LoadData();

        public void OnFormDataChaned(ToDoItem toDoItem, bool? visible = null)
        {
            ToDoItemForm.SetFormData(toDoItem);
            if (visible.HasValue)
                ToDoItemForm.TriggerForm(visible.Value);
        }
    }
}