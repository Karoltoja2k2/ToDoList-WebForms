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
        private const string ItemIdAttribute = "ItemId";

        private const string IsDoneCheckboxId = "IsDoneCheckBoxWithItemId";

        private IToDoItemRepository _repository;

        protected int CurrentToDoItemId
        {
            get => (int)ViewState[nameof(CurrentToDoItemId)];
            set
            {
                ViewState[nameof(CurrentToDoItemId)] = value;
            }       
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            _repository = new ToDoItemRepository();
            if (!IsPostBack)
            {
                CurrentToDoItemId = 0;
            }
        }

        protected void AddItemHandler(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var form = new ToDoItem
            {
                Id = 0,
                Title = TitleTextBox.Text,
                Description = DescriptionTextBox.Text,
                CreatedBy = 1,
                DueDate = DueDatePicker.selected,
            };

            _repository.Add(form);
            ClearForm();
            GridView1.DataBind();
        }

        protected void UpdateItemHandler(object sender, EventArgs e)
        {
            if (!Page.IsValid) return;

            var form = new ToDoItem
            {
                Id = CurrentToDoItemId,
                Title = TitleTextBox.Text,
                Description = DescriptionTextBox.Text,
                DueDate = DueDatePicker.selected,
            };

            _repository.Update(form);
            GridView1.DataBind();
            ClearForm();
        }

        protected void CancelUpdateHandler(object sender, EventArgs e) =>
            ClearForm();

        protected void IsDoneCheckBoxChangedHandler(object sender, EventArgs e)
        {
            var checkBox = (CheckBox)sender;
            var id = checkBox?.Attributes[ItemIdAttribute];

            if (!int.TryParse(id, out var idValue))
            {
                return;
            }

            _repository.UpdateIsDone(idValue, checkBox.Checked);
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            const string DeleteCommandName = "Delete";
            if (e.CommandName.Equals(DeleteCommandName, StringComparison.OrdinalIgnoreCase))
            {
                var id = GetIdFromRowEvent(e);
                _repository.Delete(id);
                GridView1.DataBind();
                e.Handled = true;

                if (CurrentToDoItemId == id)
                    ClearForm();
                
                return;
            }

            const string EditCommandName = "Edit";
            if (e.CommandName.Equals(EditCommandName, StringComparison.OrdinalIgnoreCase))
            {
                var id = GetIdFromRowEvent(e);
                SetFormData(_repository.GetById(id));
                e.Handled = true;
                UpdatePanel1.DataBind();
                return;
            }
        }

        private void ClearForm() => 
            SetFormData(new ToDoItem { DueDate = DateTime.Now });

        private void SetFormData(ToDoItem item)
        {
            CurrentToDoItemId = item.Id;
            TitleTextBox.Text = item.Title;
            DescriptionTextBox.Text = item.Description;
            DueDatePicker.selected = item.DueDate;
        }

        private int GetIdFromRowEvent(GridViewCommandEventArgs e)
        {
            var row = ((GridView)e.CommandSource).Rows[int.Parse((string)e.CommandArgument)];
            var txtSomething = row.FindControl(IsDoneCheckboxId);
            var cb = (CheckBox)txtSomething;
            return int.Parse(cb.Attributes[ItemIdAttribute]);
        }
    }
}