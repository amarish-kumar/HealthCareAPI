﻿using log4net;
using Newtonsoft.Json;
using OMC.DAL.Library;
using System;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace OMCApi
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        public static readonly ILog Log =
              LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            log4net.Config.XmlConfigurator.Configure();
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            Exception ex = Server.GetLastError();
            DataAccessBase objDataAccessBase = new DataAccessBase();
            var error = new OMC.Models.ErrorLog
            {
                Message = ex.Message,
                ExceptionType = ex.GetType().ToString(),
                Source = ex.Source,
                StackTrace = ex.StackTrace,
            };
            Log.Error(JsonConvert.SerializeObject(error));
            objDataAccessBase.LogError(error);
        }
    }
}
