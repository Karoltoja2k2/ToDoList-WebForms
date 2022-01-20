using System;
using System.Web.UI;
using ToDoList.Helper;
using ToDoList.View;

namespace ToDoList.UserControl
{
    public delegate void FilterChanged();

    public partial class ResultDisplayFilter : System.Web.UI.UserControl, IResultDisplayFilterView
    {
        public FilterChanged FilterChangedEvent;

        #region view properties

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

        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                IsDone = "0";
                Title = string.Empty;
                Description = string.Empty;
                DueDateFrom = DateTime.Now.ToMonthStart();
                DueDateTo = DateTime.Now.ToMonthEnd();
                LoadData();
            }

            DueDateFromFilter.OnDateChangedEvent += DateChangedHandler;
            DueDateToFilter.OnDateChangedEvent += DateChangedHandler;
        }

        protected void DateChangedHandler() =>
            LoadData();

        protected void FilterFieldChanged(object sender, EventArgs e) =>
            DateChangedHandler();

        public void LoadData() =>
            FilterChangedEvent?.Invoke();
    }
}