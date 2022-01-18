using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using ToDoList.Presenter;
using ToDoList.View;

namespace ToDoList
{
    public partial class Default : Page, IDefaultView
    {
        private const string ItemIdAttribute = "ItemId";

        private const string IsDoneCheckboxId = "IsDoneCheckBoxWithItemId";

        private IDefaultViewPresenter _presenter;

        public int FormId
        {
            get => (int)ViewState[nameof(FormId)];
            set => ViewState[nameof(FormId)] = value;
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

        public void UpdateResultDisplay()
        {
            GridView1.DataBind();
        }

        public void TriggerFormVisibility(bool visible)
        {
            ContentWraper1.IsHidden = !visible;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _presenter = new DefaultViewPresenter(this);
            if (!IsPostBack)
            {
                FormId = 0;
            }
        }

        protected void SubmitHandler(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;
            _presenter.SubmitForm();
        }

        protected void CancelUpdateHandler(object sender, EventArgs e) =>
            _presenter.ClearForm();

        protected void IsDoneCheckBoxChangedHandler(object sender, EventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var id = GetIdFromControlWithItemId(checkBox);
            _presenter.UpdateIsDone(id, checkBox.Checked);
        }

        protected void ResultDisplayRowCommand(object sender, GridViewCommandEventArgs e)
        {
            var row = ((GridView)e.CommandSource).Rows[int.Parse((string)e.CommandArgument)];
            var itemId = GetIdFromControlWithItemId((WebControl)row.FindControl(IsDoneCheckboxId));
            e.Handled = _presenter.OnRowCommand(itemId, e.CommandName);
        }

        private int GetIdFromControlWithItemId(WebControl control)
        {
            var id = control?.Attributes[ItemIdAttribute];
            if (!int.TryParse(id, out var idValue))
                throw new Exception();

            return idValue;
        }
    }
}