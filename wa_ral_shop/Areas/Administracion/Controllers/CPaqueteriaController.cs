using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wa_ral_shop.Models.Utilerias;
using wa_ral_shop.Models.Repositorios.Administracion;
using wa_ral_shop.Models.Anonymous.Administracion;
using wa_ral_shop.Models.Anonymous.Catalogos;
using wa_ral_shop.Models.Anonymous;
using System.Data;
using System.IO;

namespace wa_ral_shop.Areas.Administracion.Controllers
{
    public class CPaqueteriaController : Controller
    {
        // GET: Administracion/CPaqueteria
        public ActionResult CPaqueteria()
        {
            ActionResult actionResult = null;
            actionResult = View();

            RepositorioCPaqueteria repositorioCPaqueteria = new RepositorioCPaqueteria();
            ContentResultObject contentResultObject = new ContentResultObject();
            DataTable dtPaqueteria = new DataTable();
            List<ComboAnonymous> lstPaqueterias = new List<ComboAnonymous>();
            ComboAnonymous comboAnonymous = new ComboAnonymous();

            try
            {
                dtPaqueteria = repositorioCPaqueteria.SelectPaqueterias();
                foreach (DataRow dr in dtPaqueteria.Rows)
                {
                    comboAnonymous = new ComboAnonymous();
                    comboAnonymous.Id = dr[0].ToString();
                    comboAnonymous.Dato = dr[1].ToString();
                    lstPaqueterias.Add(comboAnonymous);
                }
                ViewData["cmbPaqueterias"] = lstPaqueterias;
            }
            catch (Exception Ex)
            {
                contentResultObject.Codigo = "Error";
                contentResultObject.Mensaje = Ex.Message;
                actionResult = Json(new { codigo = contentResultObject.Codigo, mensaje = contentResultObject.Mensaje });
            }

            return actionResult;
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Alta(Int16 IdPaqueteria, string Comentario)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCPaqueteria repositorioCPaqueteria = new RepositorioCPaqueteria();
            ComentarioPaqueteriaAnonymous comentarioPaqueteriaAnonymous = new ComentarioPaqueteriaAnonymous();
            string Mensaje = string.Empty;
            comentarioPaqueteriaAnonymous.IdPaqueteria = IdPaqueteria;
            comentarioPaqueteriaAnonymous.Comentario = Comentario;
            try
            {
                Mensaje = repositorioCPaqueteria.Alta(comentarioPaqueteriaAnonymous) > 0 ? "Agregado Correctamente" : "Error";
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
        public ActionResult Buscar(Int16 IdPaqueteria, string Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCPaqueteria repositorioCPaqueteria = new RepositorioCPaqueteria();
            ComentarioPaqueteriaAnonymous comentarioPaqueteriaAnonymous = new ComentarioPaqueteriaAnonymous();
            string Mensaje = string.Empty;
            comentarioPaqueteriaAnonymous.IdPaqueteria = IdPaqueteria;
            comentarioPaqueteriaAnonymous.EstatusSTR = Estatus;
            DataTable dtCPaqueteria = new DataTable();

            try
            {
                dtCPaqueteria = repositorioCPaqueteria.Buscar(comentarioPaqueteriaAnonymous);
                List<ComentarioPaqueteriaAnonymous> lstCPaqueteriaAnonymous = new List<ComentarioPaqueteriaAnonymous>();
                ComentarioPaqueteriaAnonymous CPaqueteriaAnonymous;
                foreach (DataRow dr in dtCPaqueteria.Rows)
                {
                    CPaqueteriaAnonymous = new ComentarioPaqueteriaAnonymous();
                    CPaqueteriaAnonymous.PaqueteriaAnonymous = new PaqueteriaAnonymous();
                    CPaqueteriaAnonymous.Id = Int16.Parse(dr["Id"].ToString());
                    CPaqueteriaAnonymous.IdPaqueteria = Int16.Parse(dr["IdPaqueteria"].ToString());
                    CPaqueteriaAnonymous.PaqueteriaAnonymous.Nombre = dr["Nombre"].ToString();
                    CPaqueteriaAnonymous.Comentario = dr["Comentario"].ToString();
                    CPaqueteriaAnonymous.FechaHoraCaptura = DateTime.Parse(dr["FechaHoraCaptura"].ToString());
                    CPaqueteriaAnonymous.Estatus = Boolean.Parse(dr["Estatus"].ToString());
                    CPaqueteriaAnonymous.EstatusSTR = Boolean.Parse(dr["Estatus"].ToString()) == true ? "Activo" : "Inactivo";
                    lstCPaqueteriaAnonymous.Add(CPaqueteriaAnonymous);
                }

                ViewData["Total"] = lstCPaqueteriaAnonymous.Count;
                var gridCategorias = RenderRazorViewToString(this.ControllerContext, "ListaCPaqueterias", lstCPaqueteriaAnonymous);

                actionResult = Json(new
                {
                    ListaCat = gridCategorias
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
        public ActionResult Editar(Int16 Id, Int16 IdPaqueteria, string Comentario, bool Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCPaqueteria repositorioCPaqueteria = new RepositorioCPaqueteria();
            ComentarioPaqueteriaAnonymous comentarioPaqueteriaAnonymous = new ComentarioPaqueteriaAnonymous();
            string Mensaje = string.Empty;
            comentarioPaqueteriaAnonymous.Id = Id;
            comentarioPaqueteriaAnonymous.IdPaqueteria = IdPaqueteria;
            comentarioPaqueteriaAnonymous.Comentario = Comentario;
            comentarioPaqueteriaAnonymous.Estatus = Estatus;
            try
            {
                Mensaje = repositorioCPaqueteria.Editar(comentarioPaqueteriaAnonymous) < 0 ? "Modificado Correctamente" : "Error";
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
            RepositorioCPaqueteria repositorioCPaqueteria = new RepositorioCPaqueteria();
            string Mensaje = string.Empty;

            try
            {
                Mensaje = repositorioCPaqueteria.Eliminar(Id) == -1 ? "Modificado Correctamente" : "Error";
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