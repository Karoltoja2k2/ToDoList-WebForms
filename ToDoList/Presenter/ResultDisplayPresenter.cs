using System;
using System.Collections.Generic;
using ToDoList.DataLayer.Model;
using ToDoList.DataLayer.Repository;
using ToDoList.View;

namespace ToDoList.Presenter
{
    public interface IResultDisplayPresenter : IPresenterBase<IResultDisplayView>
    {
        void SetResultDisplayData();

        void SetFormData(int toDoItemId);

        void DeleteToDoItem(int toDoItemId);

        void UpdateIsDone(int toDoItemId, bool isDone);

        bool OnRowCommand(int id, string commandName);
    }

    public class ResultDisplayPresenter : PresenterBase<IResultDisplayView>, IResultDisplayPresenter
    {
        private readonly IToDoItemRepository _repository;

        public ResultDisplayPresenter(IToDoItemRepository repository)
        {
            _repository = repository;
        }

        public void SetResultDisplayData()
        {
            var (total, items) = _repository.Get(_view.UserId,
                int.Parse(_view.IsDone),
                _view.Title,
                _view.Description,
                _view.DueDateFrom,
                _view.DueDateTo,
                _view.CurrentPage,
                _view.Amount);

            _view.SetDataSource(items);
            _view.Total = total;
        }

        public void SetFormData(int toDoItemId)
        {
            _view.FormDataChanged(_repository.Get(toDoItemId), true);
        }

        public void DeleteToDoItem(int toDoItemId)
        {
            _repository.Delete(toDoItemId);
            _view.ResultDisplayDataChanged();
            _view.FormDataChanged(new ToDoItem { DueDate = DateTime.Now });
        }

        public void UpdateIsDone(int toDoItemId, bool isDone)
        {
            _repository.UpdateIsDone(toDoItemId, isDone);
            _view.ResultDisplayDataChanged();
        }

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
    }
};