using System;
using System.Collections.Generic;
using ToDoList.DataLayer.Model;
using ToDoList.DataLayer.Repository;
using ToDoList.View;

namespace ToDoList.Presenter
{
    public class DefaultViewPresenter : IDefaultViewPresenter
    {
        private readonly IDefaultView _view;

        private readonly IToDoItemRepository _toDoItemRepository;

        public DefaultViewPresenter(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }

        public DefaultViewPresenter(IDefaultView view)
            : this(new ToDoItemRepository())
        {
            _view = view;
        }

        public void SetFormData(int toDoItemId)
        {
            SetFormData(_toDoItemRepository.GetById(toDoItemId));
            _view.TriggerFormVisibility(true);
        }

        public void SubmitForm()
        {
            var form = new ToDoItem
            {
                Id = _view.FormId,
                Title = _view.FormTitle,
                Description = _view.FormDescription,
                CreatedBy = 1,
                DueDate = _view.FormDueDate,
            };

            if (form.Id == 0)
            {
                _toDoItemRepository.Add(form);
            }
            else
            {
                _toDoItemRepository.Update(form);
            }

            ClearForm();
            _view.UpdateResultDisplay();
        }

        public void UpdateIsDone(int toDoItemId, bool isDone)
        {
            _toDoItemRepository.UpdateIsDone(toDoItemId, isDone);
        }

        public void DeleteToDoItem(int toDoItemId)
        {
            _toDoItemRepository.Delete(toDoItemId);
            _view.UpdateResultDisplay();
            if (_view.FormId == toDoItemId)
                ClearForm();
        }

        public void ClearForm() =>
            SetFormData(new ToDoItem { DueDate = DateTime.Now });

        public bool OnRowCommand(int id, string commandName)
        {
            const string DeleteCommandName = "Delete";
            if (commandName.Equals(DeleteCommandName, StringComparison.OrdinalIgnoreCase))
            {
                DeleteToDoItem(id);
                return true;
            }

            const string EditCommandName = "Edit";
            if (commandName.Equals(EditCommandName, StringComparison.OrdinalIgnoreCase))
            {
                SetFormData(id);
                return true;
            }

            return false;
        }

        private void SetFormData(ToDoItem item)
        {
            _view.FormId = item.Id;
            _view.FormTitle = item.Title;
            _view.FormDescription = item.Description;
            _view.FormDueDate = item.DueDate;
        }
    }
}