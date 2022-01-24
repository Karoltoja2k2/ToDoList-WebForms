using System;
using System.Collections.Generic;
using ToDoList.DataLayer.Model;
using ToDoList.DataLayer.Repository;
using ToDoList.View;

namespace ToDoList.Presenter
{
    public class ResultDisplayPresenter
    {
        private readonly IResultDisplayView _view;

        private readonly IToDoItemRepository _repository;

        public ResultDisplayPresenter(IToDoItemRepository repository)
        {
            _repository = repository;
        }

        public ResultDisplayPresenter(IResultDisplayView view)
            :this(new ToDoItemRepository())
        {
            _view = view;
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

        public void EditRowCommand(int toDoItemId)
        {
            _view.SetFormData(toDoItemId);
        }

        public void DeleteToDoItem(int toDoItemId)
        {
            _repository.Delete(toDoItemId);
            _view.OnRowDelete();
        }

        public void UpdateIsDone(int toDoItemId, bool isDone)
        {
            _repository.UpdateIsDone(toDoItemId, isDone);
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
                EditRowCommand(id);
                return true;
            }

            return false;
        }
    }
};