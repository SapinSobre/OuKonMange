using OuKonMange3.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace OuKonMange3
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);    

            IDatabaseInitializer<BddContext> init = new InitOuKonMange3();
            Database.SetInitializer(init);
            init.InitializeDatabase(new BddContext());            
        }
    }
}
