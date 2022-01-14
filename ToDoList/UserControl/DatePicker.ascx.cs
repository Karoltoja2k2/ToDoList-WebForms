using System;
using System.Web.UI;

namespace ToDoList.UserControl
{
    public partial class DatePicker : System.Web.UI.UserControl
    {
        public DateTime selected
        {
            get => CalendarControl.SelectedDate;
            set {
                CalendarControl.SelectedDate = value;
                CalendarValueTextBox.Text = CalendarControl.SelectedDate.ToString("dd.MM.yyyy");
            }
        }

        protected string UniqueId = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            UniqueId = Guid.NewGuid().ToString("N");
            CalendarValueTextBox.Attributes["onClick"] = $"CalendarValueTextBox_ClientClicked_{UniqueId}()";
            if (!Page.IsPostBack)
            {
                selected = DateTime.Now;
            }
        }

        protected void DateTimeFormatValidator(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            if (!DateTime.TryParse(args.Value, out var _))
            {
                args.IsValid = false;
            }
        }

        protected void CalendarSelectionChangeHandler(object sender, EventArgs e)
        {
            selected = CalendarControl.SelectedDate;
            ChangeCalendarVisibility(false);
        }

        private void ChangeCalendarVisibility(bool visible)
        {
            CalendarState.Text = visible ? "flex" : "none";
            CalendarContainer.Style["display"] = CalendarState.Text;
        }
    }
}