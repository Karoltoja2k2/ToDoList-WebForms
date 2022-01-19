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
        public string ButtonValue { get => ButtonLabel.Text; set => ButtonLabel.Text = value; }

        public bool IsHidden
        {
            get => (bool)ViewState[$"{nameof(IsHidden)}_{ID}"];
            set => ViewState[$"{nameof(IsHidden)}_{ID}"] = value;
        }

        public bool DefaultIsHidden { get; set; } = false;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                IsHidden = DefaultIsHidden;
            }
        }

        protected void TriggerElem(object sender, EventArgs e)
        {
            IsHidden = !IsHidden;
        }
    }
}