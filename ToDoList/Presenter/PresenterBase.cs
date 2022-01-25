using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ToDoList.Presenter
{
    public class PresenterBase<T> : IPresenterBase<T>
    {
        protected T _view { get; private set; }

        public void SetView(T view) =>
            _view = view;
    }
}