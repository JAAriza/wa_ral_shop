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
    public class CarritoController : Controller
    {
        // GET: Catalogos/Carrito
        public ActionResult Carrito()
        {
            return View();
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Alta(int IdProducto)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCarrito repositorioCarrito = new RepositorioCarrito();
            CarritoAnonymous carritoAnonymous = new CarritoAnonymous();
            string Mensaje = string.Empty;
            carritoAnonymous.IdProducto = IdProducto;
            carritoAnonymous.IdCliente = int.Parse(Session["Ide"].ToString());
            try
            {
                Mensaje = repositorioCarrito.Alta(carritoAnonymous) > 0 ? "Agregado Correctamente" : "Error";
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
        public ActionResult AltaC(int IdProducto, int Cantidad)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCarrito repositorioCarrito = new RepositorioCarrito();
            CarritoAnonymous carritoAnonymous = new CarritoAnonymous();
            string Mensaje = string.Empty;
            carritoAnonymous.IdProducto = IdProducto;
            carritoAnonymous.IdCliente = int.Parse(Session["Ide"].ToString());
            carritoAnonymous.Cantidad = Cantidad;
            try
            {
                Mensaje = repositorioCarrito.AltaC(carritoAnonymous) > 0 ? "Agregado Correctamente" : "Error";
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
            RepositorioCarrito repositorioCarrito = new RepositorioCarrito();

            string Mensaje = string.Empty;
            DataTable dtCarrito = new DataTable();

            try
            {
                dtCarrito = repositorioCarrito.Buscar(int.Parse(Session["Ide"].ToString()));
                List<CarritoAnonymous> lstCarritoAnonymous = new List<CarritoAnonymous>();
                CarritoAnonymous carritoAnonymous;
                foreach (DataRow dr in dtCarrito.Rows)
                {
                    carritoAnonymous = new CarritoAnonymous();
                    carritoAnonymous.ProductoA = new ProductoAnonymous();
                    carritoAnonymous.Id = int.Parse(dr["Id"].ToString());
                    carritoAnonymous.IdProducto = int.Parse(dr["IdProducto"].ToString());
                    carritoAnonymous.ProductoA.Nombre = dr["Nombre"].ToString();
                    carritoAnonymous.IdCliente = int.Parse(dr["IdCliente"].ToString());
                    lstCarritoAnonymous.Add(carritoAnonymous);
                }

                ViewData["Total"] = lstCarritoAnonymous.Count;
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
            RepositorioCarrito repositorioCarrito = new RepositorioCarrito();
            string Mensaje = string.Empty;

            try
            {
                Mensaje = repositorioCarrito.Eliminar(int.Parse(Session["Ide"].ToString()), IdProducto) == -1 ? "Modificado Correctamente" : "Error";
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