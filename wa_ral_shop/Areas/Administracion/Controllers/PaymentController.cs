using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
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
using Openpay;

namespace wa_ral_shop.Areas.Administracion.Controllers
{
    public class PaymentController : Controller
    {
        // GET: Administracion/Payment
        public ActionResult Payment()
        {
            return View();
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Buscar(int Id)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            DataTable dataTable = new DataTable();
            RepositorioCliente repositorioCliente = new RepositorioCliente();
            try
            {
                dataTable = repositorioCliente.BuscarPorId(Id);
                ClienteAnonymous clienteAnonymous = new ClienteAnonymous();
                foreach (DataRow dr in dataTable.Rows)
                {
                    clienteAnonymous.Id = Int32.Parse(dr["Id"].ToString());
                    clienteAnonymous.Nombre = dr["Nombre"].ToString();
                    clienteAnonymous.APaterno = dr["APaterno"].ToString();
                    clienteAnonymous.AMaterno = dr["AMaterno"].ToString();
                    clienteAnonymous.Estrellas = byte.Parse(dr["Estrellas"].ToString());
                    clienteAnonymous.Telefono = dr["Telefono"].ToString();
                    clienteAnonymous.EMail = dr["EMail"].ToString();
                    clienteAnonymous.Estatus = Boolean.Parse(dr["Estatus"].ToString());
                    clienteAnonymous.EstatusSTR = Boolean.Parse(dr["Estatus"].ToString()) == true ? "Activo" : "Inactivo";
                    clienteAnonymous.CustomerId = dr["CustomerId"].ToString();
                }
                Pagos pagos = new Pagos();
                pagos.ConsultarCliente(clienteAnonymous.CustomerId);
                ContentResultObject.Codigo = "Cliente consultado";
                ContentResultObject.Mensaje = clienteAnonymous.CustomerId;
                actionResult = Json(new { codigo = ContentResultObject.Codigo, mensaje = ContentResultObject.Mensaje });
            }
            catch (Exception ex)
            {
                ContentResultObject.Codigo = "Error";
                ContentResultObject.Mensaje = ex.Message;
                actionResult = Json(new { codigo = ContentResultObject.Codigo, mensaje = ContentResultObject.Mensaje });
                throw;
            }              
            
            return actionResult;
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult AgregarCustomer(int Id, string CustomerId)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCliente repositorioCliente = new RepositorioCliente();
            string Mensaje = string.Empty;
            
            try
            {
                if (repositorioCliente.EditarCustomer(Id, CustomerId)< 0)
                {
                    Mensaje = "Modificado Correctamente";
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
        public ActionResult BuscarPaises()
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioPais repositorioPais = new RepositorioPais();
            DataTable dtPaises = new DataTable();
            List<ComboAnonymous> lstPaises = new List<ComboAnonymous>();
            ComboAnonymous cmbPaises;
            string Mensaje = string.Empty;


            //OpenpayAPI openpayAPI = new OpenpayAPI(api_key, merchant_id, country "MX", production bool)
            OpenpayAPI openpayAPI = new OpenpayAPI("mcpqgoe22dmeeukfcajw", "pk_dbdf44828124408aa3b9c227795846a9", "MX");
            
            openpayAPI.Production = false; // Default value = false

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



    }
}