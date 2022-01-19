using System;
using System.Web.UI.WebControls;
using ToDoList.DataLayer.Model;
using ToDoList.Presenter;
using ToDoList.View;

namespace ToDoList.UserControl
{
    public partial class ResultDisplay : System.Web.UI.UserControl, IResultDisplayView
    {
        private ResultDisplayPresenter _presenter;

        public ToDoItemFormDataChangedHandler ToDoItemFormDataChangedEvent;

        public ResultDisplayDataChangedHandler ResultDisplayDataChangedEvent;

        private const string ItemIdAttribute = "ItemId";

        private const string IsDoneCheckboxId = "IsDoneCheckBoxWithItemId";

        public void BindData()
        {
            GridView1.DataBind();
        }

        public void FormDataChanged(ToDoItem toDoItem, bool isVisible)
        {
            ToDoItemFormDataChangedEvent?.Invoke(toDoItem, isVisible);
        }

        public void ResultDisplayDataChanged()
        {
            ResultDisplayDataChangedEvent?.Invoke();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new ResultDisplayPresenter(this);
        }

        protected void IsDoneCheckBoxChangedHandler(object sender, EventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var id = GetIdFromControlWithItemId(checkBox);
            _presenter.UpdateIsDone(id, checkBox.Checked);
        }

        protected void ResultDisplayRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var row = ((GridView)e.CommandSource).Rows[int.Parse((string)e.CommandArgument)];
            var itemId = GetIdFromControlWithItemId((WebControl)row.FindControl(IsDoneCheckboxId));
            e.Handled = _presenter.OnRowCommand(itemId, e.CommandName);
        }

        private int GetIdFromControlWithItemId(WebControl control)
        {
            var id = control?.Attributes[ItemIdAttribute];
            if (!int.TryParse(id, out var idValue))
                throw new Exception();

            return idValue;
        }
    }
}