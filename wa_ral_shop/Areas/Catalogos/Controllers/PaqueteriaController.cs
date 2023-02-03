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
    public class PaqueteriaController : Controller
    {
        // GET: Catalogos/Paqueteria
        public ActionResult Paqueteria()
        {
            return View();
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Alta(string Nombre, string Estrellas)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioPaqueteria repositorioPaqueteria = new RepositorioPaqueteria();
            PaqueteriaAnonymous PaqueteriaAnonymous = new PaqueteriaAnonymous();
            string Mensaje = string.Empty;
            PaqueteriaAnonymous.Nombre = Nombre;
            PaqueteriaAnonymous.Estrellas = Estrellas;
            try
            {
                Mensaje = repositorioPaqueteria.Alta(PaqueteriaAnonymous) > 0 ? "Agregado Correctamente" : "Error";
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
        public ActionResult Buscar(string Nombre, /*string Estrellas,*/ string Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioPaqueteria repositorioPaqueteria = new RepositorioPaqueteria();
            PaqueteriaAnonymous PaqueteriaAnonymous = new PaqueteriaAnonymous();
            string Mensaje = string.Empty;
            PaqueteriaAnonymous.Nombre = Nombre;
            //PaqueteriaAnonymous.Estrellas = Estrellas;
            PaqueteriaAnonymous.EstatusSTR = Estatus;
            DataTable dtPaqueteria = new DataTable();

            try
            {
                dtPaqueteria = repositorioPaqueteria.Buscar(PaqueteriaAnonymous);
                List<PaqueteriaAnonymous> lstPaqueteriaAnonymous = new List<PaqueteriaAnonymous>();
                PaqueteriaAnonymous PaqueteriaA = new PaqueteriaAnonymous();
                foreach (DataRow dr in dtPaqueteria.Rows)
                {
                    PaqueteriaA = new PaqueteriaAnonymous();
                    PaqueteriaA.Id = Int16.Parse(dr["Id"].ToString());
                    PaqueteriaA.Nombre = dr["Nombre"].ToString();
                    PaqueteriaA.FechaHoraCaptura = DateTime.Parse(dr["FechaHoraCaptura"].ToString());
                    PaqueteriaA.Estrellas = dr["Estrellas"].ToString();
                    PaqueteriaA.Estatus = Boolean.Parse(dr["Estatus"].ToString());
                    PaqueteriaA.EstatusSTR = Boolean.Parse(dr["Estatus"].ToString()) == true ? "Activo" : "Inactivo";
                    lstPaqueteriaAnonymous.Add(PaqueteriaA);
                }

                ViewData["Total"] = lstPaqueteriaAnonymous.Count;
                var gridPaqueterias = RenderRazorViewToString(this.ControllerContext, "ListaPaqueteria", lstPaqueteriaAnonymous);

                actionResult = Json(new
                {
                    ListaCat = gridPaqueterias
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
        public ActionResult Editar(Int16 Id, string Nombre, string Estrellas, bool Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioPaqueteria repositorioPaqueteria = new RepositorioPaqueteria();
            PaqueteriaAnonymous PaqueteriaAnonymous = new PaqueteriaAnonymous();
            string Mensaje = string.Empty;
            PaqueteriaAnonymous.Nombre = Nombre;
            PaqueteriaAnonymous.Id = Id;
            PaqueteriaAnonymous.Estrellas = Estrellas;
            PaqueteriaAnonymous.Estatus = Estatus;
            try
            {
                Mensaje = repositorioPaqueteria.Editar(PaqueteriaAnonymous) < 0 ? "Modificado Correctamente" : "Error";
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
            RepositorioPaqueteria repositorioPaqueteria = new RepositorioPaqueteria();
            string Mensaje = string.Empty;

            try
            {
                Mensaje = repositorioPaqueteria.Eliminar(Id) == -1 ? "Modificado Correctamente" : "Error";
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