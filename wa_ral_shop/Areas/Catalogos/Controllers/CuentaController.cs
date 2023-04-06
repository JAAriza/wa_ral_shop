using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wa_ral_shop.Models.Utilerias;
using wa_ral_shop.Models.Repositorios.Catalogos;
using wa_ral_shop.Models.Repositorios.Administracion;
using wa_ral_shop.Models.Anonymous.Administracion;
using wa_ral_shop.Models.Anonymous.Catalogos;
using System.Data;
using System.IO;
using wa_ral_shop.Models.Anonymous;

namespace wa_ral_shop.Areas.Catalogos.Controllers
{
    public class CuentaController : ControllerMaster
    {
        // GET: Catalogos/Cuenta
        public ActionResult Cuenta()
        {
            ActionResult actionResult = null;
            actionResult = SesionN("Cuenta");
            if (actionResult == null)
            {
                actionResult = View();
            }
            return actionResult;
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Buscar()
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCuenta repositorioCuenta = new RepositorioCuenta();
            string Mensaje = string.Empty;
            DataTable dtCliente = new DataTable();

            try
            {
                dtCliente = repositorioCuenta.Buscar(int.Parse(Session["Ide"].ToString()));
                List<CDireccionAnonymous> lstCDireccionA = new List<CDireccionAnonymous>();
                CDireccionAnonymous cDireccion;
                foreach (DataRow dr in dtCliente.Rows)
                {
                    cDireccion = new CDireccionAnonymous();
                    cDireccion.Id = int.Parse(dr[0].ToString());
                    cDireccion.Nombre = dr[1].ToString();
                    cDireccion.Telefono = dr[2].ToString();
                    cDireccion.CP = int.Parse(dr[3].ToString());
                    cDireccion.EntreCalle = dr[4].ToString();
                    cDireccion.YCalle = dr[5].ToString();
                    cDireccion.Calle = dr[6].ToString();
                    cDireccion.NExterior = dr[7].ToString();
                    cDireccion.NInterior = dr[8].ToString();
                    cDireccion.Colonia = dr[9].ToString();
                    cDireccion.Municipio = dr[10].ToString();
                    cDireccion.Estado = dr[11].ToString();
                    cDireccion.Descripcion = dr[12].ToString();
                    lstCDireccionA.Add(cDireccion);
                }

                ViewData["Total"] = lstCDireccionA.Count;
                var gridUbicaciones = RenderRazorViewToString(this.ControllerContext, "ListaUbicacion", lstCDireccionA);

                ClienteAnonymous clienteAnonymous = new ClienteAnonymous();
                clienteAnonymous.Nombre = Session["Nombre"].ToString();
                clienteAnonymous.APaterno = Session["APaterno"].ToString();
                clienteAnonymous.AMaterno = Session["AMAterno"].ToString();
                clienteAnonymous.Telefono = Session["Telefono"].ToString();
                clienteAnonymous.EMail = Session["EMail"].ToString();

                actionResult = Json(new
                {
                    ListaCat = gridUbicaciones, Cliente = clienteAnonymous
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
        public ActionResult ActualizarCliente(string Nombre, string APaterno, string AMaterno,
            string Telefono, string EMail)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            string Mensaje = string.Empty;

            if (Nombre == Session["Nombre"].ToString() && APaterno == Session["APaterno"].ToString()
                && AMaterno == Session["AMaterno"].ToString() && Telefono == Session["Telefono"].ToString()
                && EMail == Session["EMail"].ToString())
            {
                Mensaje = "Modificado Correctamente";
            }
            else
            {
                RepositorioCuenta repositorioCuenta = new RepositorioCuenta();
                RepositorioCliente repositorioCliente = new RepositorioCliente();
                ClienteAnonymous clienteAnonymous = new ClienteAnonymous();                
                string EMailEnc;

                clienteAnonymous.Id = int.Parse(Session["Ide"].ToString());
                clienteAnonymous.Nombre = Nombre;
                clienteAnonymous.APaterno = APaterno;
                clienteAnonymous.AMaterno = AMaterno;
                clienteAnonymous.Telefono = Telefono;
                clienteAnonymous.EMail = EMail;
                EMailEnc = EncYDec.EncryptPlainTextToCipherText(EMail);

                try
                {
                    if (repositorioCuenta.Editar(clienteAnonymous) < 0)
                    {
                        Mensaje = repositorioCliente.EditarU(clienteAnonymous, EMailEnc) < 0 ? "Modificado Correctamente" : "Error";
                    }
                    else
                    {
                        Mensaje = "Error";
                    }
                }
                catch (Exception Ex)
                {
                    ContentResultObject.Codigo = "Error";
                    ContentResultObject.Mensaje = Ex.Message;
                    actionResult = Json(new { codigo = ContentResultObject.Codigo, mensaje = ContentResultObject.Mensaje });
                }
            }

            actionResult = Json(new { codigo = Mensaje });

            return actionResult;
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
                actionResult = Json(new { mensaje = Mensaje, Edo = Estado, Mpio = Municipio });
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
        public ActionResult GDireccion(int CP, string Colonia, string Calle
            , string NExterior, string NInterior, string Nombre, string Telefono
                , string Descripcion, string EntreCalle, string YCalle)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCDireccion repositorioCDireccion = new RepositorioCDireccion();
            CDireccionAnonymous cDireccionAnonymous = new CDireccionAnonymous();
            string Mensaje = string.Empty;

            cDireccionAnonymous.IdCliente = int.Parse(Session["Ide"].ToString());
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

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Editar(int Id,string Nombre, string Telefono, int CP, string Colonia, string Calle,
            string NExterior, string NInterior, string EntreCalle, string YCalle, string Descripcion)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCuenta repositorioCuenta = new RepositorioCuenta();
            CDireccionAnonymous cDireccionAnonymous = new CDireccionAnonymous();
            string Mensaje = string.Empty;

            cDireccionAnonymous.Id = Id;
            cDireccionAnonymous.Nombre = Nombre;
            cDireccionAnonymous.Telefono = Telefono;
            cDireccionAnonymous.CP = CP;
            cDireccionAnonymous.Colonia = Colonia;
            cDireccionAnonymous.Calle = Calle;
            cDireccionAnonymous.NExterior = NExterior;
            cDireccionAnonymous.NInterior = NInterior;
            cDireccionAnonymous.EntreCalle = EntreCalle;
            cDireccionAnonymous.YCalle = YCalle;
            cDireccionAnonymous.Descripcion = Descripcion;

            try
            {
                Mensaje = repositorioCuenta.EditarUbicacion(cDireccionAnonymous) < 0 ? Mensaje = "Modificado Correctamente" : Mensaje = "Error";
                actionResult = Json(new { codigo = Mensaje });
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
            RepositorioCuenta repositorioCuenta = new RepositorioCuenta();
            string Mensaje = string.Empty;

            try
            {
                Mensaje = repositorioCuenta.Eliminar(Id) == -1 ? "Modificado Correctamente" : "Error";
                
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