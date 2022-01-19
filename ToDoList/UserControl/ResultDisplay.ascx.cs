using System;
using System.Web.UI.WebControls;
using ToDoList.DataLayer.Model;
using ToDoList.Helper;
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
        
        public string IsDone
        {
            get => IsDoneFilter.SelectedValue;
            set => IsDoneFilter.SelectedValue = value;
        }

        public string Title
        {
            get => TitleFilter.Text;
            set => TitleFilter.Text = value;
        }

        public string Description
        {
            get => DescriptionFilter.Text;
            set => DescriptionFilter.Text = value;
        }

        public DateTime DueDateFrom
        {
            get => DueDateFromFilter.selected;
            set => DueDateFromFilter.selected = value;
        }

        public DateTime DueDateTo
        {
            get => DueDateToFilter.selected;
            set => DueDateToFilter.selected = value;
        }

        public object DataSource
        {
            get => GridView1.DataSource;
            set
            {
                GridView1.DataSource = value;
                GridView1.DataBind();
            }
        }

        public void RefreshData()
        {
            _presenter.SetResultDisplayData();
        }

        public void FormDataChanged(ToDoItem toDoItem, bool? isVisible = null)
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
            if (!Page.IsPostBack)
            {
                IsDone = "0";
                Title = string.Empty;
                Description = string.Empty;
                DueDateFrom = DateTime.Now.ToMonthStart();
                DueDateTo = DateTime.Now.ToMonthEnd();
                RefreshData();
            }

            DueDateFromFilter.OnDateChangedEvent += DateChangedHandler;
            DueDateToFilter.OnDateChangedEvent += DateChangedHandler;
        }

        protected void DateChangedHandler() => RefreshData();

        protected void FilterFieldChanged(object sender, EventArgs e) =>
            DateChangedHandler();

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