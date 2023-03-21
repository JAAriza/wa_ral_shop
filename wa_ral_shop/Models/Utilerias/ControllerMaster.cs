using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace wa_ral_shop.Models.Utilerias
{
    public class ControllerMaster : Controller
    {
        public ActionResult SesionN(string screenName)
        {
            ActionResult actionResult = null;
            if (Session.Count <= 5)
            {
                actionResult = RedirectToAction("Login", "Sesion", new { area = "" });
            }
            if (!string.IsNullOrEmpty(screenName))
            {
                int Permiso = int.Parse(Session[screenName].ToString());
                if (Permiso == 0)
                {
                    actionResult = RedirectToAction("Login", "Sesion", new { area = "" });
                }
            }

            return actionResult;
        }
    }
}