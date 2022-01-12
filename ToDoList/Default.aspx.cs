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

        protected void Page_Load(object sender, EventArgs e)
        {
            _repository = new ToDoItemRepository();
            if (!Page.IsPostBack)
            {
                toDoItems = _repository.Get();
                CalendarValueTextBox.Text = DateTime.Now.ToString("d.M.yyyy");
                CalendarValueTextBox.Attributes.Add("onClick", "txtBox1_ClientClicked()");
            }

            CalendarContainer.Style["display"] = CalendarState.Text;
        }

        protected void SubmitHandler(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            _repository.Add(new ToDoItem()
            {
                Title = TitleTextBox.Text,
                Description = DescriptionTextBox.Text,  
                Status = StatusDropDownList.SelectedIndex,
                CreatedBy = 1,
                DueDate = DateTime.Parse(CalendarValueTextBox.Text),
            });

            GridView1.DataBind();
            ChangeCalendarVisibility(false);
        }

        protected void CalendarSelectionChangeHandler(object sender, EventArgs e)
        {
            CalendarValueTextBox.Text = DueDateCalendar.SelectedDate.ToString("d.M.yyyy");
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

        protected void GridView1_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Delete")
            {
                var arg = e.CommandArgument;
            }
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

        protected void DeleteItem(object sender, EventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var id = checkBox?.Attributes["ItemId"];

            if (!int.TryParse(id, out var idValue))
            {
                return;
            }

            _repository.Delete(idValue);
        }
    }
}