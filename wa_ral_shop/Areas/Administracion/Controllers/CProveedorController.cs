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
    public class CProveedorController : Controller
    {
        // GET: Administracion/CProveedor
        public ActionResult CProveedor()
        {
            ActionResult actionResult = null;
            actionResult = View();

            RepositorioCProveedor repositorioCProveedor = new RepositorioCProveedor();
            ContentResultObject contentResultObject = new ContentResultObject();
            DataTable dtProveedor = new DataTable();
            List<ComboAnonymous> lstProveedores = new List<ComboAnonymous>();
            ComboAnonymous comboAnonymous = new ComboAnonymous();

            try
            {
                dtProveedor = repositorioCProveedor.SelectProveedores();
                foreach (DataRow dr in dtProveedor.Rows)
                {
                    comboAnonymous = new ComboAnonymous();
                    comboAnonymous.Id = dr[0].ToString();
                    comboAnonymous.Dato = dr[1].ToString();
                    lstProveedores.Add(comboAnonymous);
                }
                ViewData["cmbProveedores"] = lstProveedores;
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
        public ActionResult Alta(int IdProveedor, string Comentario)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCProveedor repositorioCProveedor = new RepositorioCProveedor();
            CProveedorAnonymous cProveedorAnonymous = new CProveedorAnonymous();
            string Mensaje = string.Empty;
            cProveedorAnonymous.IdProveedor = IdProveedor;
            cProveedorAnonymous.Comentario = Comentario;
            try
            {
                Mensaje = repositorioCProveedor.Alta(cProveedorAnonymous) > 0 ? "Agregado Correctamente" : "Error";
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
        public ActionResult Buscar(int IdProveedor, string Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCProveedor repositorioCProveedor = new RepositorioCProveedor();
            CProveedorAnonymous cProveedorAnonymous = new CProveedorAnonymous();
            string Mensaje = string.Empty;
            cProveedorAnonymous.IdProveedor = IdProveedor;
            cProveedorAnonymous.EstatusSTR = Estatus;
            DataTable dtCProveedor = new DataTable();

            try
            {
                dtCProveedor = repositorioCProveedor.Buscar(cProveedorAnonymous);
                List<CProveedorAnonymous> lstCProveedorAnonymous = new List<CProveedorAnonymous>();
                CProveedorAnonymous cPAnonymous;
                foreach (DataRow dr in dtCProveedor.Rows)
                {
                    cPAnonymous = new CProveedorAnonymous();
                    cPAnonymous.proveedorAnonymous = new ProveedorAnonymous();
                    cPAnonymous.Id = int.Parse(dr["Id"].ToString());
                    cPAnonymous.IdProveedor = Int16.Parse(dr["IdProveedor"].ToString());
                    cPAnonymous.proveedorAnonymous.Nombre = dr["Nombre"].ToString();
                    cPAnonymous.Comentario = dr["Comentario"].ToString();
                    cPAnonymous.FechaHoraCaptura = DateTime.Parse(dr["FechaHoraCaptura"].ToString());
                    cPAnonymous.Estatus = Boolean.Parse(dr["Estatus"].ToString());
                    cPAnonymous.EstatusSTR = Boolean.Parse(dr["Estatus"].ToString()) == true ? "Activo" : "Inactivo";
                    lstCProveedorAnonymous.Add(cPAnonymous);
                }

                ViewData["Total"] = lstCProveedorAnonymous.Count;
                var gridCProveedores = RenderRazorViewToString(this.ControllerContext, "ListaCProveedores", lstCProveedorAnonymous);

                actionResult = Json(new
                {
                    ListaCat = gridCProveedores
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
        public ActionResult Editar(int Id, int IdProveedor, string Comentario, bool Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCProveedor repositorioCProveedor = new RepositorioCProveedor();
            CProveedorAnonymous cProveedorAnonymous = new CProveedorAnonymous();
            string Mensaje = string.Empty;
            cProveedorAnonymous.Id = Id;
            cProveedorAnonymous.IdProveedor = IdProveedor;
            cProveedorAnonymous.Comentario = Comentario;
            cProveedorAnonymous.Estatus = Estatus;
            try
            {
                Mensaje = repositorioCProveedor.Editar(cProveedorAnonymous) < 0 ? "Modificado Correctamente" : "Error";
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
            RepositorioCProveedor repositorioCProveedor = new RepositorioCProveedor();
            string Mensaje = string.Empty;

            try
            {
                Mensaje = repositorioCProveedor.Eliminar(Id) == -1 ? "Modificado Correctamente" : "Error";
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