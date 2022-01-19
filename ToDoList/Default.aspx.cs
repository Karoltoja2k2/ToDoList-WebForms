using System;
using System.Web.UI;
using ToDoList.DataLayer.Model;

namespace ToDoList
{
    public delegate void ResultDisplayDataChangedHandler();

    public delegate void ToDoItemFormDataChangedHandler(ToDoItem item, bool? visible);

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

        private void OnResultDisplayDataChanged()
        {
            ResultDisplay.LoadData();
        }

        public void OnFormDataChaned(ToDoItem toDoItem, bool? visible = null)
        {
            ToDoItemForm.SetFormData(toDoItem);
            if (visible.HasValue)
                ToDoItemForm.TriggerForm(visible.Value);
        }
    }
}