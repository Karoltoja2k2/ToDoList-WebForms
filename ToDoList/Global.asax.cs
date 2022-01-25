using Microsoft.AspNet.WebFormsDependencyInjection.Unity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using ToDoList.DataLayer;
using ToDoList.DataLayer.Repository;
using ToDoList.Presenter;
using Unity;
using Unity.Lifetime;

namespace ToDoList
{
    public class Global : HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            var container = this.AddUnity();

            container.RegisterType<IToDoItemRepository, ToDoItemRepository>();
            container.RegisterType<IUserRepository, UserRepository>();

            container.RegisterType<IResultDisplayPresenter, ResultDisplayPresenter>();
            container.RegisterType<IDefaultViewPresenter, DefaultViewPresenter>();
            container.RegisterType<IToDoItemFormPresenter, ToDoItemFormPresenter>();

            container.RegisterType<ToDoListDbContext>();
        }

        protected void Session_Start(object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {

        }

        protected void Application_Error(object sender, EventArgs e)
        {

        }

        protected void Session_End(object sender, EventArgs e)
        {

        }

        protected void Application_End(object sender, EventArgs e)
        {

        }
    }
}