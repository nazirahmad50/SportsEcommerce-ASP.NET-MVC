using SportsEcommerce.Domain.Entities;
using SportsEcommerce.WebUI.Infrastructure;
using SportsEcommerce.WebUI.Infrastructure.Binders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SportsEcommerce.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            DependencyResolver.SetResolver(new NinjectDependencyResolver());

            // need to tell the MVC framework that i can use the CartModelBinder class to create an instance of Cart
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
        }
    }
}
