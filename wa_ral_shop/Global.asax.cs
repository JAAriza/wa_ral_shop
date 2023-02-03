using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace wa_ral_shop
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        //Agregado manualmente para prevenir que cuando se cierre sesion intente volver y que ninguna
        //pantalla funcione, ya se realizaron pruebas de funcionamiento y con este cambio 
        //hace que el login funcione mas mejor
        protected void Application_BeginRequest()
        {
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();
        }


        //Agregado manualmente
        //protected void Application_EndRequest()
        //{
        //    if (Context.Items["AjaxPermissionDenied"] is bool)
        //    {
        //        Context.Response.StatusCode = 401;
        //        Context.Response.End();
        //    }
        //}
    }

    //Descomentar cuando se agregue login
    //public class SessionExpireAttribute : ActionFilterAttribute
    //{
    //    public override void OnActionExecuting(ActionExecutingContext filterContext)
    //    {
    //        HttpContext ctx = HttpContext.Current;
    //        if (ctx.Session != null)
    //        {
    //            if (ctx.Session["Usuario"] == null || ctx.Session.IsNewSession)
    //            {
    //                if (filterContext.HttpContext.Request.IsAjaxRequest())
    //                {
    //                    filterContext.HttpContext.Response.ClearContent();
    //                    filterContext.HttpContext.Items["AjaxPermissionDenied"] = true;
    //                }
    //                else
    //                {
    //                    filterContext.Result = new RedirectResult("~/Sesion/Login");
    //                }
    //            }
    //        }
    //        base.OnActionExecuting(filterContext);
    //    }
    //}

}
