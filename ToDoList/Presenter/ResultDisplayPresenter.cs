﻿using System;
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

        public void SetFormData(int toDoItemId)
        {
            _view.FormDataChanged(_repository.GetById(toDoItemId), true);
        }

        public void DeleteToDoItem(int toDoItemId)
        {
            _repository.Delete(toDoItemId);

            _view.ResultDisplayDataChanged();
            _view.FormDataChanged(new ToDoItem { DueDate = DateTime.Now }, false);
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
                SetFormData(id);
                return true;
            }

            return false;
        }
    }
};