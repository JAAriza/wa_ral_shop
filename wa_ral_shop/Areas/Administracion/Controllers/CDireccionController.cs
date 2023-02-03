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
    public class CDireccionController : Controller
    {
        // GET: Administracion/CDireccion
        public ActionResult CDireccion()
        {
            return View();
        }
        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult BuscarColonias(int CP)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCDireccion repositorioCDireccion = new RepositorioCDireccion();
            DataTable dtColonias = new DataTable();
            List<ComboAnonymous> lstColonias = new List<ComboAnonymous>();
            ComboAnonymous cmbColonias;
            string Mensaje = string.Empty;

            try
            {
                dtColonias = repositorioCDireccion.BuscarColoniasPorCP(CP);
                if (dtColonias.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtColonias.Rows)
                    {
                        cmbColonias = new ComboAnonymous();
                        cmbColonias.Dato = dr[0].ToString();
                        lstColonias.Add(cmbColonias);
                    }
                    Mensaje = "Colonias Encontradas";
                }
                else
                {
                    ContentResultObject.Codigo = "SinDatos";
                    ContentResultObject.Mensaje = "Sin datos encontrados";
                }
                actionResult = Json(new { mensaje = Mensaje, comboColonias = lstColonias });
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
        public ActionResult BuscarEdoyMpio(int CP)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCDireccion repositorioCDireccion = new RepositorioCDireccion();
            DataTable dtEdoMpio = new DataTable();
            string Estado = string.Empty;
            string Municipio = string.Empty;
            string Mensaje = string.Empty;

            try
            {
                dtEdoMpio = repositorioCDireccion.BuscarEdoyMpioPorCP(CP);
                if (dtEdoMpio.Rows.Count > 0)
                {
                    Estado = dtEdoMpio.Rows[0][0].ToString();
                    Municipio = dtEdoMpio.Rows[0][1].ToString();
                    Mensaje = "Estado y Municipio Encontrados";
                }
                else
                {
                    ContentResultObject.Codigo = "SinDatos";
                    ContentResultObject.Mensaje = "Sin datos encontrados";
                }
                actionResult = Json(new { mensaje = Mensaje, Edo = Estado, Mpio = Municipio  });
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
        public ActionResult GDireccion(int IdCliente, int CP, string Colonia, string Calle
            , string NExterior, string NInterior, string Nombre, string Telefono
                , string Descripcion, string EntreCalle, string YCalle)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCDireccion repositorioCDireccion = new RepositorioCDireccion();
            CDireccionAnonymous cDireccionAnonymous = new CDireccionAnonymous();
            string Mensaje = string.Empty;

            cDireccionAnonymous.IdCliente = IdCliente;
            cDireccionAnonymous.CP = CP;
            cDireccionAnonymous.Colonia = Colonia;
            cDireccionAnonymous.Calle = Calle;
            cDireccionAnonymous.NExterior = NExterior;
            cDireccionAnonymous.NInterior = NInterior;
            cDireccionAnonymous.Nombre = Nombre;
            cDireccionAnonymous.Telefono = Telefono;
            cDireccionAnonymous.Descripcion = Descripcion;
            cDireccionAnonymous.EntreCalle = EntreCalle;
            cDireccionAnonymous.YCalle = YCalle;

            try
            {
                Mensaje = repositorioCDireccion.Alta(cDireccionAnonymous) > 0 ? "Agregado Correctamente" : "Error";
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