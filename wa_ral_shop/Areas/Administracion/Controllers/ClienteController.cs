using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wa_ral_shop.Models.Utilerias;
using wa_ral_shop.Models.Repositorios.Administracion;
using wa_ral_shop.Models.Anonymous.Catalogos;
using System.Data;
using System.IO;

namespace wa_ral_shop.Areas.Administracion.Controllers
{
    public class ClienteController : Controller
    {
        // GET: Administracion/Cliente
        public ActionResult Cliente()
        {
            return View();
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Alta(string Nombre, string APaterno, string AMaterno, byte Estrellas,
            string Telefono, string EMail, string Pass)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCliente repositorioCliente = new RepositorioCliente();
            ClienteAnonymous clienteAnonymous = new ClienteAnonymous();
            int Id = 0;            
            string Mensaje = string.Empty;
            string Password;
            string EMailEnc;

            clienteAnonymous.Nombre = Nombre;
            clienteAnonymous.APaterno = APaterno;
            clienteAnonymous.AMaterno = AMaterno;
            clienteAnonymous.Estrellas = Estrellas;
            clienteAnonymous.Telefono = Telefono;
            clienteAnonymous.EMail = EMail;
            Password = EncYDec.EncryptPlainTextToCipherText(Pass);
            EMailEnc = EncYDec.EncryptPlainTextToCipherText(EMail);

            try
            {
                Id = repositorioCliente.Alta(clienteAnonymous);
                if (Id > 0)
                {
                    if (repositorioCliente.AltaU(Id, Password, EMailEnc) > 0)
                    {
                        Mensaje = "Agregado Correctamente";
                    }
                    else
                    {
                        repositorioCliente.EliminarFalloU(Id);
                        Mensaje = "Error";
                    }
                }
                else
                {
                    Mensaje = "Error";
                }
                
                actionResult = Json(new { mensaje = Mensaje,  Ide = Id});
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
        public ActionResult Buscar(string Nombre, string APaterno, string AMaterno, string Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCliente repositorioCliente = new RepositorioCliente();
            ClienteAnonymous clienteAnonymous = new ClienteAnonymous();
            string Mensaje = string.Empty;
            clienteAnonymous.Nombre = Nombre;
            clienteAnonymous.APaterno = APaterno;
            clienteAnonymous.AMaterno = AMaterno;
            clienteAnonymous.EstatusSTR = Estatus;
            DataTable dtCliente = new DataTable();

            try
            {
                dtCliente = repositorioCliente.Buscar(clienteAnonymous);
                List<ClienteAnonymous> lstClienteAnonymous = new List<ClienteAnonymous>();
                ClienteAnonymous clienteA;
                foreach (DataRow dr in dtCliente.Rows)
                {
                    clienteA = new ClienteAnonymous();
                    clienteA.Id = Int32.Parse(dr["Id"].ToString());
                    clienteA.Nombre = dr["Nombre"].ToString();
                    clienteA.APaterno = dr["APaterno"].ToString();
                    clienteA.AMaterno = dr["AMaterno"].ToString();
                    clienteA.Estrellas = byte.Parse(dr["Estrellas"].ToString());
                    clienteA.Telefono = dr["Telefono"].ToString();
                    clienteA.EMail = dr["EMail"].ToString();
                    clienteA.FechaHoraCaptura = DateTime.Parse(dr["FechaHoraCaptura"].ToString());
                    clienteA.Estatus = Boolean.Parse(dr["Estatus"].ToString());
                    clienteA.EstatusSTR = Boolean.Parse(dr["Estatus"].ToString()) == true ? "Activo" : "Inactivo";
                    lstClienteAnonymous.Add(clienteA);
                }

                ViewData["Total"] = lstClienteAnonymous.Count;
                var gridClientes = RenderRazorViewToString(this.ControllerContext, "ListaClientes", lstClienteAnonymous);

                actionResult = Json(new
                {
                    ListaCat = gridClientes
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

        //[HttpPost]
        ////[ValidateInput(false)]
        //public ActionResult BuscarPorId(int Id)
        //{
        //    ContentResultObject ContentResultObject = new ContentResultObject();
        //    ActionResult actionResult = null;
        //    RepositorioCliente repositorioCliente = new RepositorioCliente();
        //    string Mensaje = string.Empty;
        //    DataTable dtCliente = new DataTable();

        //    try
        //    {
        //        dtCliente = repositorioCliente.BuscarPorId(Id);
        //        List<ClienteAnonymous> lstClienteAnonymous = new List<ClienteAnonymous>();
        //        ClienteAnonymous clienteA;
        //        foreach (DataRow dr in dtCliente.Rows)
        //        {
        //            clienteA = new ClienteAnonymous();
        //            clienteA.Id = Int32.Parse(dr["Id"].ToString());
        //            clienteA.Nombre = dr["Nombre"].ToString();
        //            clienteA.APaterno = dr["APaterno"].ToString();
        //            clienteA.AMaterno = dr["AMaterno"].ToString();
        //            clienteA.Estrellas = byte.Parse(dr["Estrellas"].ToString());
        //            clienteA.Telefono = dr["Telefono"].ToString();
        //            clienteA.EMail = dr["EMail"].ToString();
        //            clienteA.FechaHoraCaptura = DateTime.Parse(dr["FechaHoraCaptura"].ToString());
        //            clienteA.Estatus = Boolean.Parse(dr["Estatus"].ToString());
        //            clienteA.EstatusSTR = Boolean.Parse(dr["Estatus"].ToString()) == true ? "Activo" : "Inactivo";
        //            lstClienteAnonymous.Add(clienteA);
        //        }
        //        Session
        //        //ViewData["Total"] = lstClienteAnonymous.Count;
        //        //var gridClientes = RenderRazorViewToString(this.ControllerContext, "ListaClientes", lstClienteAnonymous);

        //        //actionResult = Json(new
        //        //{
        //            //ListaCat = gridClientes
        //        //});
        //    }
        //    catch (Exception Ex)
        //    {
        //        ContentResultObject.Codigo = "Error";
        //        ContentResultObject.Mensaje = Ex.Message;
        //        actionResult = Json(new { codigo = ContentResultObject.Codigo, mensaje = ContentResultObject.Mensaje });
        //    }
        //    return actionResult;
        //}

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Editar(int Id, string Nombre , string APaterno, string AMaterno, byte Estrellas,
            string Telefono, string EMail, bool Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCliente repositorioCliente = new RepositorioCliente();
            ClienteAnonymous clienteAnonymous = new ClienteAnonymous();
            string Mensaje = string.Empty;
            string EMailEnc;

            clienteAnonymous.Id = Id;
            clienteAnonymous.Nombre = Nombre;
            clienteAnonymous.APaterno = APaterno;
            clienteAnonymous.AMaterno = AMaterno;
            clienteAnonymous.Estrellas = Estrellas;
            clienteAnonymous.Telefono = Telefono;
            clienteAnonymous.EMail = EMail;
            clienteAnonymous.Estatus = Estatus;
            EMailEnc = EncYDec.EncryptPlainTextToCipherText(EMail);

            try
            {
                if (repositorioCliente.Editar(clienteAnonymous, EMailEnc) < 0)
                {
                    Mensaje = repositorioCliente.EditarU(clienteAnonymous, EMailEnc) < 0 ? "Modificado Correctamente" : "Error";
                }
                else
                {
                    Mensaje = "Error";
                }
                 
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
            RepositorioCliente repositorioCliente = new RepositorioCliente();
            string Mensaje = string.Empty;

            try
            {
                if (repositorioCliente.Eliminar(Id) == -1)
                {
                    Mensaje = repositorioCliente.EliminarUC(Id) == -1 ? "Modificado Correctamente" : "Error";
                }
                else
                {
                    Mensaje = "Error";
                }
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