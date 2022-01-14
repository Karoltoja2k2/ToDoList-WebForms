using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace ToDoList.UserControl
{
    public partial class ContentWraper : System.Web.UI.UserControl
    {
        public string ButtonValue;

        public string TargetId { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {

        }
    }
}