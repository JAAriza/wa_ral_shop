using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wa_ral_shop.Controllers;
//using wa_ral_shop.Models.Anonymous.Administracion;
//using wa_ral_shop.Models.Anonymous.Catalogos;

namespace wa_ral_shop.Filters
{
    public class VerificaSession : ActionFilterAttribute
    {
        //private ClienteUsuarioAnonymous ClienteUsuario;
        //private ClienteAnonymous cliente;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            try
            {
                base.OnActionExecuting(filterContext);
                //ClienteUsuario.EMail = HttpContext.Current.Session["EMail"].ToString();
                //ClienteUsuario.Password = HttpContext.Current.Session["Password"].ToString();
                //string EMail = string.Empty;
                //EMail = HttpContext.Current.Session["EMail"].ToString();
                if (string.IsNullOrEmpty(HttpContext.Current.Session["EMail"] as string))//string.IsNullOrEmpty(EMail))
                    {
                    if (filterContext.Controller is SesionController == false)
                    {
                        filterContext.HttpContext.Response.Redirect("/Sesion/Login");
                    }
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Sesion/Login");
            }            
        }
    }
}