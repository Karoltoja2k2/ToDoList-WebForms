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
        private readonly IUserRepository _userRepository;

        public ToDoItems(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!int.TryParse(Request.QueryString[Default.QueryStringUserIdKey], out var userId))
                    Response.Redirect("Default.aspx");

                ResultDisplay.SetUserId(userId);
                ToDoItemForm.SetUserId(userId);
                Master.UserName = _userRepository.Get(userId).Name;

                LoadComplete += (s, ev) => Page.DataBind();
            }

            ToDoItemForm.ResultDisplayDataChangedEvent += OnResultDisplayDataChanged;
            ResultDisplay.ToDoItemFormDataChangedEvent += OnFormDataChaned;
            ResultDisplay.ResultDisplayDataChangedEvent += OnResultDisplayDataChanged;
        }

        private void OnResultDisplayDataChanged() => ResultDisplay.ReloadData();

        public void OnFormDataChaned(ToDoItem toDoItem, bool? visible = null)
        {
            ToDoItemForm.SetFormData(toDoItem);
            if (visible.HasValue)
                ToDoItemForm.TriggerForm(visible.Value);
        }
    }
}