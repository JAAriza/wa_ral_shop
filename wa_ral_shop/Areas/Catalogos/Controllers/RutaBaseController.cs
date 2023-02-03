using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wa_ral_shop.Models.Utilerias;
using wa_ral_shop.Models.Repositorios.Catalogos;
using wa_ral_shop.Models.Anonymous.Catalogos;
using System.Data;
using System.IO;

namespace wa_ral_shop.Areas.Catalogos.Controllers
{
    public class RutaBaseController : Controller
    {
        // GET: Catalogos/RutaBase
        public ActionResult RutaBase()
        {
            return View();
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Alta(string RutaBase)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioRutaBase repositorioRutaBase = new RepositorioRutaBase();
            RutaBaseAnonymous RutaBaseAnonymous = new RutaBaseAnonymous();
            string Mensaje = string.Empty;
            RutaBaseAnonymous.RutaBase = RutaBase;
            try
            {
                Mensaje = repositorioRutaBase.Alta(RutaBaseAnonymous) > 0 ? "Agregado Correctamente" : "Error";
                actionResult = Json(new { mensaje = Mensaje });
            }
            catch (Exception Ex)
            {
                ContentResultObject.Codigo = "Error";
                ContentResultObject.Mensaje = Ex.Message;
                actionResult = Json(new { codigo = ContentResultObject.Codigo, mensaje = ContentResultObject.Mensaje });
            }
            return actionResult;
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Buscar(string RutaBase, string Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioRutaBase repositorioRutaBase = new RepositorioRutaBase();
            RutaBaseAnonymous RutaBaseAnonymous = new RutaBaseAnonymous();
            string Mensaje = string.Empty;
            RutaBaseAnonymous.RutaBase = RutaBase;
            RutaBaseAnonymous.EstatusSTR = Estatus;
            DataTable dtRutaBase = new DataTable();

            try
            {
                dtRutaBase = repositorioRutaBase.Buscar(RutaBaseAnonymous);
                List<RutaBaseAnonymous> lstRutaBaseAnonymous = new List<RutaBaseAnonymous>();
                RutaBaseAnonymous RutaBaseA = new RutaBaseAnonymous();
                foreach (DataRow dr in dtRutaBase.Rows)
                {
                    RutaBaseA = new RutaBaseAnonymous();
                    RutaBaseA.Id = byte.Parse(dr["Id"].ToString());
                    RutaBaseA.RutaBase = dr["RutaBase"].ToString();
                    RutaBaseA.FechaHoraCaptura = DateTime.Parse(dr["FechaHoraCaptura"].ToString());
                    RutaBaseA.Estatus = Boolean.Parse(dr["Estatus"].ToString());
                    RutaBaseA.EstatusSTR = Boolean.Parse(dr["Estatus"].ToString()) == true ? "Activo" : "Inactivo";
                    lstRutaBaseAnonymous.Add(RutaBaseA);
                }

                ViewData["Total"] = lstRutaBaseAnonymous.Count;
                var gridRutasBase = RenderRazorViewToString(this.ControllerContext, "ListaRutasBase", lstRutaBaseAnonymous);

                actionResult = Json(new
                {
                    ListaCat = gridRutasBase
                });
            }
            catch (Exception Ex)
            {
                ContentResultObject.Codigo = "Error";
                ContentResultObject.Mensaje = Ex.Message;
                actionResult = Json(new { codigo = ContentResultObject.Codigo, mensaje = ContentResultObject.Mensaje });
            }
            return actionResult;
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Editar(byte Id, string RutaBase, bool Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioRutaBase repositorioRutaBase = new RepositorioRutaBase();
            RutaBaseAnonymous RutaBaseAnonymous = new RutaBaseAnonymous();
            string Mensaje = string.Empty;
            RutaBaseAnonymous.RutaBase = RutaBase;
            RutaBaseAnonymous.Id = Id;
            RutaBaseAnonymous.Estatus = Estatus;
            try
            {
                Mensaje = repositorioRutaBase.Editar(RutaBaseAnonymous) < 0 ? "Modificado Correctamente" : "Error";
                actionResult = Json(new { mensaje = Mensaje });
            }
            catch (Exception Ex)
            {
                ContentResultObject.Codigo = "Error";
                ContentResultObject.Mensaje = Ex.Message;
                actionResult = Json(new { codigo = ContentResultObject.Codigo, mensaje = ContentResultObject.Mensaje });
            }
            return actionResult;
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Eliminar(byte Id)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioRutaBase repositorioRutaBase = new RepositorioRutaBase();
            string Mensaje = string.Empty;

            try
            {
                Mensaje = repositorioRutaBase.Eliminar(Id) == -1 ? "Modificado Correctamente" : "Error";
                actionResult = Json(new { mensaje = Mensaje });
            }
            catch (Exception Ex)
            {
                ContentResultObject.Codigo = "Error";
                ContentResultObject.Mensaje = Ex.Message;
                actionResult = Json(new { codigo = ContentResultObject.Codigo, mensaje = ContentResultObject.Mensaje });
            }
            return actionResult;
        }

        public static String RenderRazorViewToString(ControllerContext controllerContext, String viewName, Object model)
        {
            controllerContext.Controller.ViewData.Model = model;

            using (var sw = new StringWriter())
            {
                var ViewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var ViewContext = new ViewContext(controllerContext, ViewResult.View, controllerContext.Controller.ViewData, controllerContext.Controller.TempData, sw);
                ViewResult.View.Render(ViewContext, sw);
                ViewResult.ViewEngine.ReleaseView(controllerContext, ViewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
    }
}