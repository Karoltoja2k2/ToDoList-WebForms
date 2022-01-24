using System;
using System.Web;
using System.Web.UI;
using ToDoList.DataLayer.Model;
using ToDoList.Helper;
using ToDoList.Presenter;
using ToDoList.View;

namespace ToDoList.UserControl
{
    public partial class ToDoItemForm : System.Web.UI.UserControl, IToDoItemFormView
    {
        private ToDoItemFormPresenter _presenter;

        public event ResultDisplayDataChangedHandler ResultDisplayDataChangedEvent;

        private int _userId;

        public int UserId
        {
            get => _userId;
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

        public void SetFormId(int id) =>
            _presenter.SetFormData(id);

        public void RefreshWraper() => FormWraper.DataBind();

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new ToDoItemFormPresenter(this);
            _userId = AppSession.GetUserId();
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