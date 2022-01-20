using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using ToDoList.DataLayer.Model;
using ToDoList.Presenter;
using ToDoList.View;

namespace ToDoList.UserControl
{
    public partial class ResultDisplay : System.Web.UI.UserControl, IResultDisplayView
    {
        public ToDoItemFormDataChangedHandler ToDoItemFormDataChangedEvent;

        public ResultDisplayDataChangedHandler ResultDisplayDataChangedEvent;

        private const string ItemIdAttribute = "ItemId";

        private const string IsDoneCheckboxId = "IsDoneCheckBoxWithItemId";

        private ResultDisplayPresenter _presenter;

        #region view properties

        public int UserId { get => (int)ViewState[nameof(UserId)]; }

        public string IsDone { get => ResultDisplayFilter.IsDone; }

        public string Title { get => ResultDisplayFilter.Title; }

        public string Description { get => ResultDisplayFilter.Description; }

        public DateTime DueDateFrom { get => ResultDisplayFilter.DueDateFrom; }

        public DateTime DueDateTo { get => ResultDisplayFilter.DueDateTo; }

        public int Total { get => Pagination.Total; set => Pagination.UpdateTotal(value); }

        public int Amount { get => Pagination.Amount; }

        public int CurrentPage { get => Pagination.CurrentPage; }

        protected object DataSource
        {
            get => GridView1.DataSource;
            set
            {
                GridView1.DataSource = value;
                GridView1.DataBind();
            }
        }

        #endregion

        public void SetUserId(int value) => ViewState[nameof(UserId)] = value;

        public void SetDataSource(List<ToDoItem> data) => DataSource = data;

        public void LoadData() => _presenter.SetResultDisplayData();

        public void FilterChanged()
        {
            Pagination.CurrentPage = 1;
            _presenter.SetResultDisplayData();
        }

        public void FormDataChanged(ToDoItem toDoItem, bool? isVisible = null) =>
            ToDoItemFormDataChangedEvent?.Invoke(toDoItem, isVisible);

        public void ResultDisplayDataChanged() => ResultDisplayDataChangedEvent?.Invoke();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Pagination.CurrentPage = 1;
            }

            _presenter = new ResultDisplayPresenter(this);
            ResultDisplayFilter.FilterChangedEvent += FilterChanged;
            Pagination.PageChangedEvent += PageChangedHandler;
        }

        private void PageChangedHandler()
        {
            _presenter.SetResultDisplayData();
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