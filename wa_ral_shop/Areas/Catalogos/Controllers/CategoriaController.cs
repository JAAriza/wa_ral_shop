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
    public class CategoriaController : ControllerMaster
    {
        // GET: Catalogos/Categoria
        public ActionResult Categoria()
        {
            ActionResult actionResult = null;
            actionResult = SesionN("Categoria");
            if (actionResult == null)
            {
                actionResult = View();
            }
            return actionResult;
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Alta(string Categoria)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCategoria repositorioCategoria = new RepositorioCategoria();
            CategoriaAnonymous categoriaAnonymous = new CategoriaAnonymous();
            string Mensaje = string.Empty;
            categoriaAnonymous.Categoria = Categoria;
            try
            {                
                Mensaje = repositorioCategoria.Alta(categoriaAnonymous) > 0 ? "Agregado Correctamente" : "Error";
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
        public ActionResult Buscar(string Categoria, string Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCategoria repositorioCategoria = new RepositorioCategoria();
            CategoriaAnonymous categoriaAnonymous = new CategoriaAnonymous();
            string Mensaje = string.Empty;
            categoriaAnonymous.Categoria = Categoria;
            categoriaAnonymous.EstatusSTR = Estatus;
            DataTable dtCategoria = new DataTable();

            try
            {
                dtCategoria = repositorioCategoria.Buscar(categoriaAnonymous);
                List<CategoriaAnonymous> lstCategoriaAnonymous = new List<CategoriaAnonymous>();
                CategoriaAnonymous categoriaA = new CategoriaAnonymous();
                foreach (DataRow dr in dtCategoria.Rows)
                {
                    categoriaA = new CategoriaAnonymous();
                    categoriaA.Id = byte.Parse(dr["Id"].ToString());
                    categoriaA.Categoria = dr["Categoria"].ToString();
                    categoriaA.FechaHoraCaptura = DateTime.Parse(dr["FechaHoraCaptura"].ToString());
                    categoriaA.Estatus = Boolean.Parse(dr["Estatus"].ToString());
                    categoriaA.EstatusSTR = Boolean.Parse(dr["Estatus"].ToString()) == true ? "Activo" : "Inactivo";
                    lstCategoriaAnonymous.Add(categoriaA);
                }

                ViewData["Total"] = lstCategoriaAnonymous.Count;
                var gridCategorias = RenderRazorViewToString(this.ControllerContext, "ListaCategorias", lstCategoriaAnonymous);
                
                actionResult = Json(new { 
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
        public ActionResult Editar(byte Id, string Categoria, bool Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCategoria repositorioCategoria = new RepositorioCategoria();
            CategoriaAnonymous categoriaAnonymous = new CategoriaAnonymous();
            string Mensaje = string.Empty;
            categoriaAnonymous.Categoria = Categoria;
            categoriaAnonymous.Id = Id;
            categoriaAnonymous.Estatus = Estatus;
            try
            {
                Mensaje = repositorioCategoria.Editar(categoriaAnonymous) < 0 ? "Modificado Correctamente" : "Error";
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
            RepositorioCategoria repositorioCategoria = new RepositorioCategoria();
            string Mensaje = string.Empty;
            
            try
            {
                Mensaje = repositorioCategoria.Eliminar(Id) == -1 ? "Modificado Correctamente" : "Error";
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