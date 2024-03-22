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
    public class AlmacenController : ControllerMaster
    {
        // GET: Catalogos/Almacen
        public ActionResult Almacen()
        {
            ActionResult actionResult = null;
            actionResult = SesionN("Almacen");
            if (actionResult == null)
            {
                actionResult = View();
            }
            return actionResult;
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Alta(string Almacen)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioAlmacen repositorioAlmacen = new RepositorioAlmacen();
            AlmacenAnonymous AlmacenAnonymous = new AlmacenAnonymous();
            string Mensaje = string.Empty;
            AlmacenAnonymous.Almacen = Almacen;

            try
            {
                Mensaje = repositorioAlmacen.Alta(AlmacenAnonymous) > 0 ? "Agregado Correctamente" : "Error";
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
        public ActionResult Buscar(string Almacen, string Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioAlmacen repositorioAlmacen = new RepositorioAlmacen();
            AlmacenAnonymous AlmacenAnonymous = new AlmacenAnonymous();
            string Mensaje = string.Empty;
            AlmacenAnonymous.Almacen = Almacen;
            AlmacenAnonymous.EstatusSTR = Estatus;
            DataTable dtAlmacen = new DataTable();

            try
            {
                dtAlmacen = repositorioAlmacen.Buscar(AlmacenAnonymous);
                List<AlmacenAnonymous> lstAlmacenAnonymous = new List<AlmacenAnonymous>();
                AlmacenAnonymous AlmacenA = new AlmacenAnonymous();
                foreach (DataRow dr in dtAlmacen.Rows)
                {
                    AlmacenA = new AlmacenAnonymous();
                    AlmacenA.Id = byte.Parse(dr["Id"].ToString());
                    AlmacenA.Almacen = dr["Nombre"].ToString();
                    AlmacenA.FechaHoraCaptura = DateTime.Parse(dr["FechaHoraCaptura"].ToString());
                    AlmacenA.Estatus = Boolean.Parse(dr["Estatus"].ToString());
                    AlmacenA.EstatusSTR = Boolean.Parse(dr["Estatus"].ToString()) == true ? "Activo" : "Inactivo";
                    lstAlmacenAnonymous.Add(AlmacenA);
                }

                ViewData["Total"] = lstAlmacenAnonymous.Count;
                var gridAlmacenes = RenderRazorViewToString(this.ControllerContext, "ListaAlmacenes", lstAlmacenAnonymous);

                actionResult = Json(new
                {
                    ListaCat = gridAlmacenes
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
        public ActionResult Editar(byte Id, string Almacen, bool Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioAlmacen repositorioAlmacen = new RepositorioAlmacen();
            AlmacenAnonymous AlmacenAnonymous = new AlmacenAnonymous();
            string Mensaje = string.Empty;
            AlmacenAnonymous.Almacen = Almacen;
            AlmacenAnonymous.Id = Id;
            AlmacenAnonymous.Estatus = Estatus;
            try
            {
                Mensaje = repositorioAlmacen.Editar(AlmacenAnonymous) < 0 ? "Modificado Correctamente" : "Error";
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
            RepositorioAlmacen repositorioAlmacen = new RepositorioAlmacen();
            string Mensaje = string.Empty;

            try
            {
                Mensaje = repositorioAlmacen.Eliminar(Id) == -1 ? "Modificado Correctamente" : "Error";
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