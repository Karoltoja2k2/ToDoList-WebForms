using System;
using System.Web.UI;
using ToDoList.DataLayer.Model;

namespace ToDoList
{
    public delegate void ResultDisplayDataChangedHandler();

    public delegate void ToDoItemFormDataChangedHandler(ToDoItem item, bool visible);

    public partial class Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Page.DataBind();
            }

            ToDoItemForm.ResultDisplayDataChangedEvent += OnResultDisplayDataChanged;
            ResultDisplay.ToDoItemFormDataChangedEvent += OnFormDataChaned;
            ResultDisplay.ResultDisplayDataChangedEvent += OnResultDisplayDataChanged;
        }

        private void Test()
        {
        }

        private void OnResultDisplayDataChanged()
        {
            ResultDisplay.BindData();
        }

        public void OnFormDataChaned(ToDoItem toDoItem, bool visible)
        {
            ToDoItemForm.SetFormData(toDoItem);
            ToDoItemForm.TriggerForm(visible);
        }
    }
}