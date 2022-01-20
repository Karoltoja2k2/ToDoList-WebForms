using System;
using System.Web.UI;
using ToDoList.DataLayer.Model;
using ToDoList.Presenter;
using ToDoList.View;

namespace ToDoList.UserControl
{
    public partial class ToDoItemForm : System.Web.UI.UserControl, IToDoItemFormView
    {
        private ToDoItemFormPresenter _presenter;

        public event ResultDisplayDataChangedHandler ResultDisplayDataChangedEvent;

        public int UserId
        {
            get => (int)ViewState[nameof(UserId)];
            private set => ViewState[nameof(UserId)] = value;
        }

        public int FormId
        {
            get => (int) (ViewState[$"{nameof(FormId)}_{ID}"] ?? 0);
            set => ViewState[$"{nameof(FormId)}_{ID}"] = value;
        }

        public string FormTitle
        {
            get => TitleTextBox.Text;
            set => TitleTextBox.Text = value;
        }
        public string FormDescription
        {
            get => DescriptionTextBox.Text;
            set => DescriptionTextBox.Text = value;
        }

        public DateTime FormDueDate
        {
            get => DueDatePicker.selected;
            set => DueDatePicker.selected = value;
        }

        public void SetUserId(int value) => UserId = value;

        public void TriggerForm(bool visible) => FormWraper.IsHidden = !visible;

        public void SetFormData(ToDoItem item) => _presenter.SetFormData(item);

        public void RefreshWraper() => FormWraper.DataBind();

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new ToDoItemFormPresenter(this);
            if (!Page.IsPostBack)
                FormId = 0;
        }

        protected void SubmitHandler(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            _presenter.SubmitForm();
            ResultDisplayDataChangedEvent?.Invoke();
        }

        protected void CancelUpdateHandler(object sender, EventArgs e) =>
            _presenter.ClearForm();
    }
}