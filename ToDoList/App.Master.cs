using System;
using System.Web;
using ToDoList.Helper;

namespace ToDoList
{
    public partial class App : System.Web.UI.MasterPage
    {
        public string UserName
        {
            get => (string)HttpContext.Current.Session[AppConst.UserNameSessionKey] ?? string.Empty;
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