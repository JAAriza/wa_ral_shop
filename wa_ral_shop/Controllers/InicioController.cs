using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wa_ral_shop.Controllers
{
    [AllowAnonymous]
    public class InicioController : Controller
    {
        // GET: Inicio
        public ActionResult Principal()
        {
            return View();
        }
    }
}