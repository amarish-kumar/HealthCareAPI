﻿using System.Web.Mvc;
using System.Web.Routing;

namespace OMCWebApp
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "deleteDoctorProfile",
                url: "deleteDoctorProfile",
                defaults: new { controller = "Doctor", action = "Delete", id = UrlParameter.Optional }
            );
            routes.MapRoute(
               name: "deleteDoctorProfileResponse",
               url: "deleteDoctorProfileResponse",
               defaults: new { controller = "Doctor", action = "DoctorProfileResponse", id = UrlParameter.Optional }
           );
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
