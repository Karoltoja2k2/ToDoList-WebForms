using System;
using System.Linq;
using ToDoList.DataLayer.Model;
using ToDoList.DataLayer.Repository;
using ToDoList.View;

namespace ToDoList.Presenter
{
    public class ToDoItemFormPresenter
    {
        private readonly IToDoItemFormView _view;

        private readonly IToDoItemRepository _toDoItemRepository;

        public ToDoItemFormPresenter(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
        }

        public ToDoItemFormPresenter(IToDoItemFormView view)
            : this(new ToDoItemRepository())
        {
            _view = view;
        }

        public void SubmitForm()
        {
            var form = new ToDoItem
            {
                Id = _view.FormId,
                Title = _view.FormTitle,
                Description = _view.FormDescription,
                DueDate = _view.FormDueDate,
                AssignedTo = _view.UserId,
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
        }

        public void ClearForm() =>
            SetFormData(new ToDoItem { DueDate = DateTime.Now });

        public void SetFormData(int toDoItem)
        {
            var item = _toDoItemRepository.Get(toDoItem);
            if (item == null)
            {
                ClearForm();
                return;
            }

            SetFormData(item);
        }

        public void SetFormData(ToDoItem item)
        {
            _view.FormId = item.Id;
            _view.FormTitle = item.Title;
            _view.FormDescription = item.Description;
            _view.FormDueDate = item.DueDate;
            _view.RefreshWraper();
        }
    }
}