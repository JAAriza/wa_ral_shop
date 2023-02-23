using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wa_ral_shop.Models.Utilerias;

namespace wa_ral_shop.Controllers
{
    [AllowAnonymous]
    public class InicioController : Controller
    {
        // GET: Inicio
        public ActionResult Principal()
        {
            Info info = new Info();
            Session["TUsuario"] = info.GetTUsuario();//Se agrega para evitar error en validacion en layout
            return View();
        }
    }
}