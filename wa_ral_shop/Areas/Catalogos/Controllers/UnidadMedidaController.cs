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
    public class UnidadMedidaController : Controller
    {
        // GET: Catalogos/UnidadMedida
        public ActionResult UnidadMedida()
        {
            return View();
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Alta(string Nombre)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioUnidadMedida repositorioUnidadMedida = new RepositorioUnidadMedida();
            UnidadMedidaAnonymous UnidadMedidaAnonymous = new UnidadMedidaAnonymous();
            string Mensaje = string.Empty;
            UnidadMedidaAnonymous.Nombre = Nombre;
            try
            {                
                Mensaje = repositorioUnidadMedida.Alta(UnidadMedidaAnonymous) > 0 ? "Agregado Correctamente" : "Error";
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
        public ActionResult Buscar(string Nombre, string Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioUnidadMedida repositorioUnidadMedida = new RepositorioUnidadMedida();
            UnidadMedidaAnonymous UnidadMedidaAnonymous = new UnidadMedidaAnonymous();
            string Mensaje = string.Empty;
            UnidadMedidaAnonymous.Nombre = Nombre;
            UnidadMedidaAnonymous.EstatusSTR = Estatus;
            DataTable dtUnidadMedida = new DataTable();

            try
            {
                dtUnidadMedida = repositorioUnidadMedida.Buscar(UnidadMedidaAnonymous);
                List<UnidadMedidaAnonymous> lstUnidadMedidaAnonymous = new List<UnidadMedidaAnonymous>();
                UnidadMedidaAnonymous UnidadMedidaA = new UnidadMedidaAnonymous();
                foreach (DataRow dr in dtUnidadMedida.Rows)
                {
                    UnidadMedidaA = new UnidadMedidaAnonymous();
                    UnidadMedidaA.Id = byte.Parse(dr["Id"].ToString());
                    UnidadMedidaA.Nombre = dr["Nombre"].ToString();
                    UnidadMedidaA.FechaHoraCaptura = DateTime.Parse(dr["FechaHoraCaptura"].ToString());
                    UnidadMedidaA.Estatus = Boolean.Parse(dr["Estatus"].ToString());
                    UnidadMedidaA.EstatusSTR = Boolean.Parse(dr["Estatus"].ToString()) == true ? "Activo" : "Inactivo";
                    lstUnidadMedidaAnonymous.Add(UnidadMedidaA);
                }

                ViewData["Total"] = lstUnidadMedidaAnonymous.Count;
                var gridUnidadesMedida = RenderRazorViewToString(this.ControllerContext, "ListaUnidadesMedida", lstUnidadMedidaAnonymous);
                
                actionResult = Json(new { 
                    ListaCat = gridUnidadesMedida
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
        public ActionResult Editar(byte Id, string Nombre, bool Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioUnidadMedida repositorioUnidadMedida = new RepositorioUnidadMedida();
            UnidadMedidaAnonymous UnidadMedidaAnonymous = new UnidadMedidaAnonymous();
            string Mensaje = string.Empty;
            UnidadMedidaAnonymous.Nombre = Nombre;
            UnidadMedidaAnonymous.Id = Id;
            UnidadMedidaAnonymous.Estatus = Estatus;
            try
            {
                Mensaje = repositorioUnidadMedida.Editar(UnidadMedidaAnonymous) < 0 ? "Modificado Correctamente" : "Error";
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
            RepositorioUnidadMedida repositorioUnidadMedida = new RepositorioUnidadMedida();
            string Mensaje = string.Empty;
            
            try
            {
                Mensaje = repositorioUnidadMedida.Eliminar(Id) == -1 ? "Modificado Correctamente" : "Error";
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