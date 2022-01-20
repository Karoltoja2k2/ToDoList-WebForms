using System;

namespace ToDoList
{
    public partial class App : System.Web.UI.MasterPage
    {
        public string UserName
        {
            get => (string)ViewState[nameof(UserName)] ?? string.Empty;
            set => ViewState[nameof(UserName)] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void HeaderButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}