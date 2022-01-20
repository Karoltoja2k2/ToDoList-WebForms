using System;
using ToDoList.View;

namespace ToDoList.UserControl
{
    public delegate void PageChangedEvent();

    public partial class Pagination : System.Web.UI.UserControl, IPaginationView
    {
        public PageChangedEvent PageChangedEvent;

        public int Amount
        { 
            get => int.Parse(PagesDropDownList.SelectedValue); 
            set => PagesDropDownList.SelectedValue = value.ToString(); 
        }

        public int Total
        {
            get => (int)ViewState[nameof(Total)];
            set => ViewState[nameof(Total)] = value;
        }

        public int TotalPages
        {
            get => GetLastPageIndex();
            set => TotalPagesLabel.Text = value.ToString();
        }

        public int CurrentPage
        {
            get => int.Parse(CurrentPageLabel.Text);
            set => CurrentPageLabel.Text = value.ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        public void UpdateTotal(int value)
        {
            Total = value;
            TotalPages = GetLastPageIndex();
        }

        protected void PageChangeCommand(object sender, System.Web.UI.WebControls.CommandEventArgs e)
        {
            var currentPage = CurrentPage;
            var modifier = int.Parse((string)e.CommandArgument);
            var newPage = currentPage += modifier;
            if (newPage < 1 || newPage > TotalPages)
                return;

            CurrentPage = newPage;
            PageChangedEvent?.Invoke();
        }

        protected void AmountPerPageChanged(object sender, EventArgs e)
        {
            CurrentPage = 1;
            PageChangedEvent?.Invoke();
        }

        private int GetLastPageIndex()
        {
            var amount = Amount;
            return (Total + amount - 1) / amount;
        }
    }
}