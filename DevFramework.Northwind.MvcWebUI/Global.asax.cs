using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DevFramework.Northwind.Business.DependencyResolvers.Ninject;
using DevFramework.Core.Utilities.Mvc.Infrastructure;
using PostSharp.Patterns.Diagnostics;




namespace DevFramework.Northwind.MvcWebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(MvcApplication));
        protected void Application_Start()
        {
            log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("log4net.config")));
            //log4net.Config.XmlConfigurator.Configure();


            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes); 

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(new BusinessModule()));
            log4net.Config.XmlConfigurator.Configure();


        }
    }
}
