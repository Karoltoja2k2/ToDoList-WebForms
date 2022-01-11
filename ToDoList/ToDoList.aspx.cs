using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using ToDoList.DataLayer.Repository;

namespace ToDoList
{
    public partial class ToDoList : System.Web.UI.Page
    {
        private readonly ToDoItemRepository _repository = new ToDoItemRepository();

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public void Unnamed_InsertItem()
        {

        }
    }
}