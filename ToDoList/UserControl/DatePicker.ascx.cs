using System;
using System.Web.UI;
using ToDoList.View;

namespace ToDoList.UserControl
{
    public partial class DatePicker : System.Web.UI.UserControl
    {
        public DateTime selected
        {
            get => CalendarControl.SelectedDate;
            set {
                CalendarControl.SelectedDate = value.Date;
                CalendarValueTextBox.Text = CalendarControl.SelectedDate.ToString("dd.MM.yyyy");
            }
        }

        public bool IsHidden
        {
            get => (bool)ViewState[$"{nameof(IsHidden)}_{ID}"];
            set => ViewState[$"{nameof(IsHidden)}_{ID}"] = value;
        }


        public DateTime DefaultValue { get; set; } = DateTime.Now;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                IsHidden = true;
                selected = DefaultValue;
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
            IsHidden = !visible;
        }

        protected void OpenCalendar(object sender, EventArgs e)
        {
            ChangeCalendarVisibility(true);
        }

        protected void CloseCalendar(object sender, EventArgs e)
        {
            ChangeCalendarVisibility(false);
        }
    }
}