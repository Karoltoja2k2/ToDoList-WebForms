using System.Web;
using ToDoList.DataLayer.Repository;

namespace ToDoList.Helper
{
    public static class AppSession
    {
        public static int GetUserId()
        {
            var id = HttpContext.Current?.Session[AppConst.UserIdSessionKey] as int?;
            if (id != null)
                return id.Value;

            RedirectToLoginPage();
            return 0;
        }

        public static void CheckIfValidUserLogged()
        {
            var _userRepository = new UserRepository();
            var userId = GetUserId();
            if (_userRepository.Get(userId) is null)
                RedirectToLoginPage();
        }

        public static void RedirectToLoginPage() =>
            HttpContext.Current.Response.Redirect("Default.aspx");
    }
}