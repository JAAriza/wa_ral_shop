using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wa_ral_shop.Models.Utilerias;
using wa_ral_shop.Models.Repositorios.Administracion;
using wa_ral_shop.Models.Anonymous.Catalogos;
using wa_ral_shop.Models.Anonymous;
using wa_ral_shop.Models.Anonymous.Administracion;
using System.Data;
using System.IO;

namespace wa_ral_shop.Areas.Administracion.Controllers
{
    public class ProveedorController : Controller
    {
        // GET: Administracion/Proveedor
        public ActionResult Proveedor()
        {
            return View();
        }
        [HttpPost]
        public ActionResult BuscarPaises()
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioPais repositorioPais = new RepositorioPais();
            DataTable dtPaises = new DataTable();
            List<ComboAnonymous> lstPaises = new List<ComboAnonymous>();
            ComboAnonymous cmbPaises;
            string Mensaje = string.Empty;

            try
            {
                dtPaises = repositorioPais.BuscarPaises();
                if (dtPaises.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtPaises.Rows)
                    {
                        cmbPaises = new ComboAnonymous();
                        cmbPaises.Id = dr[0].ToString();
                        cmbPaises.Dato = dr[1].ToString();
                        lstPaises.Add(cmbPaises);
                    }
                    Mensaje = "Paises Encontrados";
                }
                else
                {
                    ContentResultObject.Codigo = "SinDatos";
                    ContentResultObject.Mensaje = "Sin datos encontrados";
                }
                actionResult = Json(new { mensaje = Mensaje, comboPaises = lstPaises });
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
        public ActionResult Alta(string Nombre, int IdPais, string Telefono, string Telefono2,
            string EMail, string EMail2, byte Estrellas)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioProveedor repositorioProveedor = new RepositorioProveedor();
            ProveedorAnonymous proveedorAnonymous = new ProveedorAnonymous();
            int Id = 0;
            string Mensaje = string.Empty;

            proveedorAnonymous.Nombre = Nombre;
            proveedorAnonymous.IdPais = IdPais;
            proveedorAnonymous.Telefono = Telefono;
            proveedorAnonymous.Telefono2 = Telefono2;
            proveedorAnonymous.EMail = EMail;
            proveedorAnonymous.EMail2 = EMail2;
            proveedorAnonymous.Estrellas = Estrellas;

            try
            {
                Id = repositorioProveedor.Alta(proveedorAnonymous);
                Mensaje = Id > 0 ? "Agregado Correctamente" : "Error";
                actionResult = Json(new { mensaje = Mensaje, Ide = Id });
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
        public ActionResult Buscar(string Nombre, int IdPais, string Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioProveedor repositorioProveedor = new RepositorioProveedor();
            ProveedorAnonymous proveedorAnonymous = new ProveedorAnonymous();
            string Mensaje = string.Empty;
            proveedorAnonymous.Nombre = Nombre;
            proveedorAnonymous.IdPais = IdPais;
            proveedorAnonymous.EstatusSTR = Estatus;
            DataTable dtProveedor = new DataTable();

            try
            {
                dtProveedor = repositorioProveedor.Buscar(proveedorAnonymous);
                List<ProveedorAnonymous> lstProveedorAnonymous = new List<ProveedorAnonymous>();
                ProveedorAnonymous ProveedorA;
                foreach (DataRow dr in dtProveedor.Rows)
                {
                    ProveedorA = new ProveedorAnonymous();
                    ProveedorA.Paises = new PaisAnonymous();
                    ProveedorA.Id = Int32.Parse(dr["Id"].ToString());
                    ProveedorA.Nombre = dr["Proveedor"].ToString();
                    ProveedorA.IdPais = int.Parse(dr["IdPais"].ToString());
                    ProveedorA.Paises.Nombre = dr["Pais"].ToString();
                    ProveedorA.Telefono = dr["Telefono"].ToString();
                    ProveedorA.Telefono2 = dr["Telefono2"].ToString();
                    ProveedorA.EMail = dr["EMail"].ToString();
                    ProveedorA.EMail2 = dr["EMail2"].ToString();
                    ProveedorA.Estrellas = Convert.ToByte(dr["Estrellas"].ToString());
                    ProveedorA.FechaHoraCaptura = DateTime.Parse(dr["FechaHoraCaptura"].ToString());
                    ProveedorA.Estatus = Boolean.Parse(dr["Estatus"].ToString());
                    ProveedorA.EstatusSTR = Boolean.Parse(dr["Estatus"].ToString()) == true ? "Activo" : "Inactivo";
                    lstProveedorAnonymous.Add(ProveedorA);
                }

                ViewData["Total"] = lstProveedorAnonymous.Count;
                var gridProveedores = RenderRazorViewToString(this.ControllerContext, "ListaProveedores", lstProveedorAnonymous);

                actionResult = Json(new
                {
                    ListaCat = gridProveedores
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
        public ActionResult Editar(int Id, string Nombre, int IdPais, string Telefono, string Telefono2,
            string EMail, string EMail2, byte Estrellas, bool Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioProveedor repositorioProveedor = new RepositorioProveedor();
            ProveedorAnonymous proveedorAnonymous = new ProveedorAnonymous();
            string Mensaje = string.Empty;
            proveedorAnonymous.Id = Id;
            proveedorAnonymous.Nombre = Nombre;
            proveedorAnonymous.IdPais = IdPais;
            proveedorAnonymous.Telefono = Telefono;
            proveedorAnonymous.Telefono2 = Telefono2;
            proveedorAnonymous.EMail = EMail;
            proveedorAnonymous.EMail2 = EMail2;
            proveedorAnonymous.Estrellas = Estrellas;
            proveedorAnonymous.Estatus = Estatus;

            try
            {
                Mensaje = repositorioProveedor.Editar(proveedorAnonymous) < 0 ? "Modificado Correctamente" : "Error";
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
            RepositorioProveedor repositorioProveedor = new RepositorioProveedor();
            string Mensaje = string.Empty;

            try
            {
                Mensaje = repositorioProveedor.Eliminar(Id) == -1 ? "Modificado Correctamente" : "Error";
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