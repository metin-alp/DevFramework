using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using DevFramework.Northwind.Business.DependencyResolvers.Ninject;
using DevFramework.Core.Utilities.Mvc.Infrastructure;
using PostSharp.Patterns.Diagnostics;
using System.Web.Security;
using DevFramework.Core.CrossCuttingConcerns.Security.Web;
using System.Security.Principal;
using System.Threading;

namespace DevFramework.Northwind.MvcWebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        //private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(MvcApplication));
        protected void Application_Start()
        {
            //log4net.Config.XmlConfigurator.Configure(new System.IO.FileInfo(Server.MapPath("log4net.config")));
            //log4net.Config.XmlConfigurator.Configure();


            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes); 

            ControllerBuilder.Current.SetControllerFactory(new NinjectControllerFactory(new BusinessModule(), new AutoMapperModule()));
            //log4net.Config.XmlConfigurator.Configure();
        }
        public override void Init()
        {
            PostAuthenticateRequest += MvcApplication_PostAuthenticateRequest;
            base.Init();
        }

        private void MvcApplication_PostAuthenticateRequest(object sender, EventArgs e)
        {
            try
            {
                var authCookie = HttpContext.Current.Request.Cookies[FormsAuthentication.FormsCookieName];
                if (authCookie == null)
                {
                    return;
                }
                var encTicket = authCookie.Value;
                if (String.IsNullOrEmpty(encTicket))
                {
                    return;
                }
                var ticket = FormsAuthentication.Decrypt(encTicket);

                var securityUtilities = new SecurityUtilities();
                var identity = securityUtilities.FormsAuthTicketToIdentity(ticket);
                var princepal = new GenericPrincipal(identity, identity.Roles);

                HttpContext.Current.User = princepal;
                Thread.CurrentPrincipal = princepal;
            }
            catch (Exception)
            {

            }
            
        }
    }
}
