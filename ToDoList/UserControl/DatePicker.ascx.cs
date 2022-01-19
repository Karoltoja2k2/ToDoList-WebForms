using System;
using System.Web.UI;

namespace ToDoList.UserControl
{
    public delegate void OnDateChanged();

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

        public OnDateChanged OnDateChangedEvent;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                IsHidden = true;
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
            OnDateChangedEvent?.Invoke();
            ChangeCalendarVisibility(false);
        }

        protected void OpenCalendar(object sender, EventArgs e)
        {
            ChangeCalendarVisibility(true);
        }

        protected void CloseCalendar(object sender, EventArgs e)
        {
            ChangeCalendarVisibility(false);
        }

        private void ChangeCalendarVisibility(bool visible)
        {
            IsHidden = !visible;
        }
    }
}