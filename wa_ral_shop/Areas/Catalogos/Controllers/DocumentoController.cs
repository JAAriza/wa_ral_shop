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
    public class DocumentoController : Controller
    {
        // GET: Catalogos/Documento
        public ActionResult Documento()
        {
            return View();
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Alta(string Documento)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioDocumento repositorioDocumento = new RepositorioDocumento();
            DocumentoAnonymous DocumentoAnonymous = new DocumentoAnonymous();
            string Mensaje = string.Empty;
            DocumentoAnonymous.Documento = Documento;
            try
            {
                Mensaje = repositorioDocumento.Alta(DocumentoAnonymous) > 0 ? "Agregado Correctamente" : "Error";
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
        public ActionResult Buscar(string Documento, string Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioDocumento repositorioDocumento = new RepositorioDocumento();
            DocumentoAnonymous DocumentoAnonymous = new DocumentoAnonymous();
            string Mensaje = string.Empty;
            DocumentoAnonymous.Documento = Documento;
            DocumentoAnonymous.EstatusSTR = Estatus;
            DataTable dtDocumento = new DataTable();

            try
            {
                dtDocumento = repositorioDocumento.Buscar(DocumentoAnonymous);
                List<DocumentoAnonymous> lstDocumentoAnonymous = new List<DocumentoAnonymous>();
                DocumentoAnonymous DocumentoA = new DocumentoAnonymous();
                foreach (DataRow dr in dtDocumento.Rows)
                {
                    DocumentoA = new DocumentoAnonymous();
                    DocumentoA.Id = Int16.Parse(dr["Id"].ToString());
                    DocumentoA.Documento = dr["Documento"].ToString();
                    DocumentoA.FechaHoraCaptura = DateTime.Parse(dr["FechaHoraCaptura"].ToString());
                    DocumentoA.Estatus = Boolean.Parse(dr["Estatus"].ToString());
                    DocumentoA.EstatusSTR = Boolean.Parse(dr["Estatus"].ToString()) == true ? "Activo" : "Inactivo";
                    lstDocumentoAnonymous.Add(DocumentoA);
                }

                ViewData["Total"] = lstDocumentoAnonymous.Count;
                var gridDocumentos = RenderRazorViewToString(this.ControllerContext, "ListaDocumentos", lstDocumentoAnonymous);

                actionResult = Json(new
                {
                    ListaCat = gridDocumentos
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
        public ActionResult Editar(Int16 Id, string Documento, bool Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioDocumento repositorioDocumento = new RepositorioDocumento();
            DocumentoAnonymous DocumentoAnonymous = new DocumentoAnonymous();
            string Mensaje = string.Empty;
            DocumentoAnonymous.Documento = Documento;
            DocumentoAnonymous.Id = Id;
            DocumentoAnonymous.Estatus = Estatus;
            try
            {
                Mensaje = repositorioDocumento.Editar(DocumentoAnonymous) < 0 ? "Modificado Correctamente" : "Error";
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
            RepositorioDocumento repositorioDocumento = new RepositorioDocumento();
            string Mensaje = string.Empty;

            try
            {
                Mensaje = repositorioDocumento.Eliminar(Id) == -1 ? "Modificado Correctamente" : "Error";
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