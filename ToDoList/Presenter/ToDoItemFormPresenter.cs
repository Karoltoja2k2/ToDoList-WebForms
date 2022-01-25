using System;
using System.Linq;
using ToDoList.DataLayer.Model;
using ToDoList.DataLayer.Repository;
using ToDoList.View;

namespace ToDoList.Presenter
{
    public interface IToDoItemFormPresenter : IPresenterBase<IToDoItemFormView>
    {
        void SubmitForm();

        void ClearForm();

        void SetFormData(ToDoItem item);
    }

    public class ToDoItemFormPresenter : PresenterBase<IToDoItemFormView>, IToDoItemFormPresenter
    {
        private readonly IToDoItemRepository _toDoItemRepository;

        public ToDoItemFormPresenter(IToDoItemRepository toDoItemRepository)
        {
            _toDoItemRepository = toDoItemRepository;
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