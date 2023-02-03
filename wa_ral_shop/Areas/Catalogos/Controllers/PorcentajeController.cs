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
    public class PorcentajeController : Controller
    {
        // GET: Catalogos/Porcentaje
        public ActionResult Porcentaje()
        {
            return View();
        }
        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Alta(string Nombre, float Porcentaje)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioPorcentaje repositorioPorcentaje = new RepositorioPorcentaje();
            PorcentajeAnonymous PorcentajeAnonymous = new PorcentajeAnonymous();
            string Mensaje = string.Empty;
            PorcentajeAnonymous.Nombre = Nombre;
            PorcentajeAnonymous.Porcentaje = Porcentaje;
            try
            {
                Mensaje = repositorioPorcentaje.Alta(PorcentajeAnonymous) > 0 ? "Agregado Correctamente" : "Error";
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
        public ActionResult Buscar(string Nombre, string Estatus)//, float Porcentaje
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioPorcentaje repositorioPorcentaje = new RepositorioPorcentaje();
            PorcentajeAnonymous PorcentajeAnonymous = new PorcentajeAnonymous();
            string Mensaje = string.Empty;
            PorcentajeAnonymous.Nombre = Nombre;
            //PorcentajeAnonymous.Porcentaje = Porcentaje;
            PorcentajeAnonymous.EstatusSTR = Estatus;
            DataTable dtPorcentaje = new DataTable();

            try
            {
                dtPorcentaje = repositorioPorcentaje.Buscar(PorcentajeAnonymous);
                List<PorcentajeAnonymous> lstPorcentajeAnonymous = new List<PorcentajeAnonymous>();
                PorcentajeAnonymous PorcentajeA = new PorcentajeAnonymous();
                foreach (DataRow dr in dtPorcentaje.Rows)
                {
                    PorcentajeA = new PorcentajeAnonymous();
                    PorcentajeA.Id = Int16.Parse(dr["Id"].ToString());
                    PorcentajeA.Nombre = dr["Nombre"].ToString();
                    PorcentajeA.FechaHoraCaptura = DateTime.Parse(dr["FechaHoraCaptura"].ToString());
                    PorcentajeA.Porcentaje = float.Parse(dr["Porcentaje"].ToString());
                    PorcentajeA.Estatus = Boolean.Parse(dr["Estatus"].ToString());
                    PorcentajeA.EstatusSTR = Boolean.Parse(dr["Estatus"].ToString()) == true ? "Activo" : "Inactivo";
                    lstPorcentajeAnonymous.Add(PorcentajeA);
                }

                ViewData["Total"] = lstPorcentajeAnonymous.Count;
                var gridPorcentajes = RenderRazorViewToString(this.ControllerContext, "ListaPorcentaje", lstPorcentajeAnonymous);

                actionResult = Json(new
                {
                    ListaCat = gridPorcentajes
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
        public ActionResult Editar(int Id, string Nombre, float Porcentaje, bool Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioPorcentaje repositorioPorcentaje = new RepositorioPorcentaje();
            PorcentajeAnonymous PorcentajeAnonymous = new PorcentajeAnonymous();
            string Mensaje = string.Empty;
            PorcentajeAnonymous.Nombre = Nombre;
            PorcentajeAnonymous.Id = Id;
            PorcentajeAnonymous.Porcentaje = Porcentaje;
            PorcentajeAnonymous.Estatus = Estatus;
            try
            {
                Mensaje = repositorioPorcentaje.Editar(PorcentajeAnonymous) < 0 ? "Modificado Correctamente" : "Error";
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
        public ActionResult Eliminar(int Id)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioPorcentaje repositorioPorcentaje = new RepositorioPorcentaje();
            string Mensaje = string.Empty;

            try
            {
                Mensaje = repositorioPorcentaje.Eliminar(Id) == -1 ? "Modificado Correctamente" : "Error";
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