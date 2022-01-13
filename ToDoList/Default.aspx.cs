using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using ToDoList.DataLayer.Model;
using ToDoList.DataLayer.Repository;

namespace ToDoList
{
    public partial class Default : System.Web.UI.Page
    {
        private IToDoItemRepository _repository;

        protected List<ToDoItem> toDoItems = new List<ToDoItem>();

        protected static ToDoItem form = new ToDoItem();

        protected void Page_Load(object sender, EventArgs e)
        {
            _repository = new ToDoItemRepository();
            if (!Page.IsPostBack)
            {
                toDoItems = _repository.Get();
                CalendarValueTextBox.Text = DateTime.Now.ToString("dd.MM.yyyy");
                CalendarValueTextBox.Attributes.Add("onClick", "txtBox1_ClientClicked()");
            }

            CalendarContainer.Style["display"] = CalendarState.Text;
        }

        protected void AddItemHandler(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            form.Id = 0;
            form.Title = TitleTextBox.Text;
            form.Description = DescriptionTextBox.Text;
            form.CreatedBy = 1;
            form.DueDate = DateTime.Parse(CalendarValueTextBox.Text);
            _repository.Add(form);
            ClearForm();

            GridView1.DataBind();
            ChangeCalendarVisibility(false);
        }

        private void ClearForm()
        {
            form.Id = 0;
            form.Title = String.Empty;
            form.Description = String.Empty;
            form.DueDate = DateTime.Now;

            TitleTextBox.Text = form.Title;
            DescriptionTextBox.Text = form.Description;
            CalendarValueTextBox.Text = form.DueDate.ToString("dd.MM.yyyy");
        }

        protected void UpdateItemHandler(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            form.Title = TitleTextBox.Text;
            form.Description = DescriptionTextBox.Text;
            form.DueDate = DateTime.Parse(CalendarValueTextBox.Text);
            _repository.Update(form);

            GridView1.DataBind();
            ChangeCalendarVisibility(false);
        }
        protected void CancelUpdateHandler(object sender, EventArgs e)
        {
            ClearForm();
        }

        protected void CalendarSelectionChangeHandler(object sender, EventArgs e)
        {
            CalendarValueTextBox.Text = DueDateCalendar.SelectedDate.ToString("dd.MM.yyyy");
            ChangeCalendarVisibility(false);
        }

        protected void CalendarCloseButton_Click(object sender, EventArgs e)
        {
            ChangeCalendarVisibility(false);
        }

        protected void CustomValidator1_ServerValidate(object source, System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            if (!DateTime.TryParse(args.Value, out var _))
            {
                args.IsValid = false;
            }
        }

        private void ChangeCalendarVisibility(bool visible)
        {
            CalendarState.Text = visible ? "flex" : "none";
            CalendarContainer.Style["display"] = CalendarState.Text;
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                var id = GetIdFromRowEvent(e);
                _repository.Delete(id);
                GridView1.DataBind();
                e.Handled = true;
                return;
            }

            if (e.CommandName == "Edit")
            {
                var id = GetIdFromRowEvent(e);
                form = _repository.GetById(id);
                TitleTextBox.Text = form.Title;
                DescriptionTextBox.Text = form.Description;
                CalendarValueTextBox.Text = form.DueDate.ToString("dd.MM.yyyy");
                e.Handled = true;
                return;
            }
        }

        private int GetIdFromRowEvent(GridViewCommandEventArgs e)
        {
            var row = ((GridView)e.CommandSource).Rows[int.Parse((string)e.CommandArgument)];
            var txtSomething = row.FindControl("IsDoneCheckBoxWithItemId");
            var cb = (CheckBox)txtSomething;
            return int.Parse(cb.Attributes["ItemId"]);
        }

        protected void IsDoneCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var id = checkBox?.Attributes["ItemId"];

            if (!int.TryParse(id, out var idValue))
            {
                return;
            }

            _repository.UpdateIsDone(idValue, checkBox.Checked);
        }
    }
}