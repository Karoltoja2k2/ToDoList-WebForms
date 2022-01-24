using System;
using ToDoList.Helper;

namespace ToDoList
{
    public partial class AddToDoItemForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            AppSession.CheckIfValidUserLogged();
            if (!IsPostBack)
            {
                Page.LoadComplete += (o, ev) =>
                {
                    var stringId = Request.QueryString["id"];
                    if (!int.TryParse(stringId, out var id))
                        id = 0;

                    ToDoItemForm.SetFormId(id);
                    Page.DataBind();
                };
            }
        }
    }
}