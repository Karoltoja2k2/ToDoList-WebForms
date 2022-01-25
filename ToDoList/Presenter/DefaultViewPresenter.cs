using ToDoList.DataLayer.Model;
using ToDoList.DataLayer.Repository;
using ToDoList.View;

namespace ToDoList.Presenter
{
    public interface IDefaultViewPresenter : IPresenterBase<IDefaultView>
    {
        void OnLogin();

        void OnRegisterClick();
    }

    public class DefaultViewPresenter : PresenterBase<IDefaultView>, IDefaultViewPresenter
    {
        private readonly IUserRepository _repository;

        public DefaultViewPresenter(IUserRepository repository)
        {
            _repository = repository;
        }

        public void OnLogin()
        {
            var userName = _view.UserName;
            var user = _repository.GetByName(userName);
            if (user == null)
            {
                _view.UserNotFound();
                return;
            }

            _view.LoginSuccess(user);
        }

        public void OnRegisterClick()
        {
            var userName = _view.UserName;
            var user = _repository.GetByName(userName);
            if (user != null)
            {
                _view.UserAlreadyExist();
                return;
            }

            user = new User { Name = userName };
            user.Id = _repository.Add(user);
            _view.RegisterSuccess(user);
        }
    }
}