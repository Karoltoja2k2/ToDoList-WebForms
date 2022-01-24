using System;
using System.Web;
using System.Web.UI;
using ToDoList.DataLayer.Model;
using ToDoList.DataLayer.Repository;
using ToDoList.Helper;

namespace ToDoList
{
    public delegate void ResultDisplayDataChangedHandler();

    public delegate void ToDoItemFormDataChangedHandler(ToDoItem item, bool? visible);

    public partial class ToDoItems : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AppSession.CheckIfValidUserLogged();
            if (!IsPostBack)
            {
                LoadComplete += (s, ev) => Page.DataBind();
            }
        }
    }
}