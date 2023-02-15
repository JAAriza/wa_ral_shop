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
    public class FavoritoController : Controller
    {
        // GET: Catalogos/Favorito
        public ActionResult Favorito()
        {
            return View();
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Alta(int IdProducto)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioFavorito repositorioFavorito = new RepositorioFavorito();
            FavoritoAnonymous favoritoAnonymous = new FavoritoAnonymous();
            string Mensaje = string.Empty;
            favoritoAnonymous.IdProducto = IdProducto;
            favoritoAnonymous.IdCliente = int.Parse(Session["Ide"].ToString());
            try
            {
                Mensaje = repositorioFavorito.Alta(favoritoAnonymous) > 0 ? "Agregado Correctamente" : "Error";
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
        public ActionResult Buscar()
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioFavorito repositorioFavorito = new RepositorioFavorito();
            
            string Mensaje = string.Empty;
            DataTable dtFavorito = new DataTable();

            try
            {
                dtFavorito = repositorioFavorito.Buscar(int.Parse(Session["Ide"].ToString()));
                List<FavoritoAnonymous> lstFavoritoAnonymous = new List<FavoritoAnonymous>();
                FavoritoAnonymous favoritoAnonymous;
                foreach (DataRow dr in dtFavorito.Rows)
                {
                    favoritoAnonymous = new FavoritoAnonymous();
                    favoritoAnonymous.ProductoA = new ProductoAnonymous();
                    favoritoAnonymous.Id = int.Parse(dr["Id"].ToString());
                    favoritoAnonymous.IdProducto = int.Parse(dr["IdProducto"].ToString());
                    favoritoAnonymous.ProductoA.Nombre = dr["Nombre"].ToString();
                    favoritoAnonymous.IdCliente = int.Parse(dr["IdCliente"].ToString());
                    lstFavoritoAnonymous.Add(favoritoAnonymous);
                }

                ViewData["Total"] = lstFavoritoAnonymous.Count;
                //var gridCategorias = RenderRazorViewToString(this.ControllerContext, "ListaCategorias", lstCategoriaAnonymous);

                //actionResult = Json(new
                //{
                //    ListaCat = gridCategorias
                //});
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
        public ActionResult Eliminar(int IdProducto)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioFavorito repositorioFavorito = new RepositorioFavorito();
            string Mensaje = string.Empty;

            try
            {
                Mensaje = repositorioFavorito.Eliminar(int.Parse(Session["Ide"].ToString()), IdProducto) == -1 ? "Modificado Correctamente" : "Error";
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
    }
}