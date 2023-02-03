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
    public class PuestoController : Controller
    {
        // GET: Catalogos/Puesto
        public ActionResult Puesto()
        {
            return View();
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Alta(string Puesto)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioPuesto repositorioPuesto = new RepositorioPuesto();
            PuestoAnonymous PuestoAnonymous = new PuestoAnonymous();
            string Mensaje = string.Empty;
            PuestoAnonymous.Puesto = Puesto;
            try
            {
                Mensaje = repositorioPuesto.Alta(PuestoAnonymous) > 0 ? "Agregado Correctamente" : "Error";
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
        public ActionResult Buscar(string Puesto, string Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioPuesto repositorioPuesto = new RepositorioPuesto();
            PuestoAnonymous PuestoAnonymous = new PuestoAnonymous();
            string Mensaje = string.Empty;
            PuestoAnonymous.Puesto = Puesto;
            PuestoAnonymous.EstatusSTR = Estatus;
            DataTable dtPuesto = new DataTable();

            try
            {
                dtPuesto = repositorioPuesto.Buscar(PuestoAnonymous);
                List<PuestoAnonymous> lstPuestoAnonymous = new List<PuestoAnonymous>();
                PuestoAnonymous PuestoA = new PuestoAnonymous();
                foreach (DataRow dr in dtPuesto.Rows)
                {
                    PuestoA = new PuestoAnonymous();
                    PuestoA.Id = Int16.Parse(dr["Id"].ToString());
                    PuestoA.Puesto = dr["Puesto"].ToString();
                    PuestoA.FechaHoraCaptura = DateTime.Parse(dr["FechaHoraCaptura"].ToString());
                    PuestoA.Estatus = Boolean.Parse(dr["Estatus"].ToString());
                    PuestoA.EstatusSTR = Boolean.Parse(dr["Estatus"].ToString()) == true ? "Activo" : "Inactivo";
                    lstPuestoAnonymous.Add(PuestoA);
                }

                ViewData["Total"] = lstPuestoAnonymous.Count;
                var gridPuestos = RenderRazorViewToString(this.ControllerContext, "ListaPuestos", lstPuestoAnonymous);

                actionResult = Json(new
                {
                    ListaCat = gridPuestos
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
        public ActionResult Editar(Int16 Id, string Puesto, bool Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioPuesto repositorioPuesto = new RepositorioPuesto();
            PuestoAnonymous PuestoAnonymous = new PuestoAnonymous();
            string Mensaje = string.Empty;
            PuestoAnonymous.Puesto = Puesto;
            PuestoAnonymous.Id = Id;
            PuestoAnonymous.Estatus = Estatus;
            try
            {
                Mensaje = repositorioPuesto.Editar(PuestoAnonymous) < 0 ? "Modificado Correctamente" : "Error";
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
        public ActionResult Eliminar(Int16 Id)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioPuesto repositorioPuesto = new RepositorioPuesto();
            string Mensaje = string.Empty;

            try
            {
                Mensaje = repositorioPuesto.Eliminar(Id) == -1 ? "Modificado Correctamente" : "Error";
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