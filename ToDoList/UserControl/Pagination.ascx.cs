using System;
using ToDoList.View;

namespace ToDoList.UserControl
{
    public delegate void PaginationEvent();

    public partial class Pagination : System.Web.UI.UserControl, IPaginationView
    {
        public PaginationEvent PageChangedEvent;

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
            get => (int)ViewState[nameof(TotalPages)];
            set => ViewState[nameof(TotalPages)] = value;
        }

        public int CurrentPage
        {
            get => (int)ViewState[nameof(CurrentPage)];
            set => ViewState[nameof(CurrentPage)] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            this.Page.LoadComplete += (s, ev) => PageLabel.Text = $" {(TotalPages == 0 ? 0 : CurrentPage)} / {TotalPages} ";
        }

        public void UpdateTotal(int value)
        {
            Total = value;
            TotalPages = GetLastPageIndex(Amount, value);
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

        private static int GetLastPageIndex(int amount, int total)
        {
            return (total + amount - 1) / amount;
        }
    }
}