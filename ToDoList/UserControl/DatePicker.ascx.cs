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

        public bool IsHidden
        {
            get => (bool)ViewState[$"{nameof(IsHidden)}_{ID}"];
            set => ViewState[$"{nameof(IsHidden)}_{ID}"] = value;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            CalendarValueTextBox.Attributes["onClick"] = $"OpenCalendar_{ID}()";
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

        protected void OpenCalendar(object sender, EventArgs e)
        {

        }
    }
}