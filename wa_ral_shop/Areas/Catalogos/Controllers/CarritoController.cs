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
            string Mensaje = string.Empty;
            Info info = new Info();
            string Ubica = info.GetIp() + "Producto" + "\\";
            DataTable dtProductosDash = new DataTable();
            RepositorioCarrito repositorioCarrito = new RepositorioCarrito();
            List<ProductoImagenAnonymous> LPIA = new List<ProductoImagenAnonymous>();
            List<ProductoAnonymous> LPA = new List<ProductoAnonymous>();
            try
            {
                dtProductosDash = repositorioCarrito.Buscar(int.Parse(Session["Ide"].ToString()));
                if (dtProductosDash.Rows.Count > 0)
                {
                    DataTable dtProd = new DataTable();
                    dtProd = dtProductosDash.DefaultView.ToTable(true, "IdProducto", "NomP", "Existencias", "PrecioVenta", "Estatus");

                    LPIA = new List<ProductoImagenAnonymous>();
                    foreach (DataRow dr in dtProductosDash.Rows)
                    {
                        ProductoImagenAnonymous PIA = new ProductoImagenAnonymous();
                        PIA.productoAnonymous = new ProductoAnonymous();
                        PIA.IdProducto = int.Parse(dr[0].ToString());
                        PIA.productoAnonymous.Nombre = dr[1].ToString();
                        PIA.productoAnonymous.PrecioVenta = decimal.Parse(dr[2].ToString());
                        PIA.productoAnonymous.Existencias = string.IsNullOrEmpty(dr[3].ToString()) ? 0 : int.Parse(dr[3].ToString());
                        PIA.Nombre = dr[4].ToString();
                        PIA.Extension = dr[5].ToString();
                        PIA.Consecutivo = byte.Parse(dr[6].ToString());
                        LPIA.Add(PIA);
                    }

                    LPA = new List<ProductoAnonymous>();
                    foreach (DataRow dr in dtProd.Rows)
                    {
                        ProductoAnonymous PA = new ProductoAnonymous();
                        PA.Id = int.Parse(dr[0].ToString());
                        PA.Nombre = dr[1].ToString();
                        PA.Existencias = string.IsNullOrEmpty(dr[2].ToString()) ? 0 : int.Parse(dr[2].ToString());
                        PA.PrecioVenta = decimal.Parse(dr[3].ToString());
                        LPA.Add(PA);
                    }
                }
                string Ruta = string.Empty;
                if (string.IsNullOrEmpty(Ubica))
                {
                    Ruta = @"C:\Mercar\Producto";
                }
                else
                {
                    Ruta = Ubica;
                }
                actionResult = Json(new { LPI = LPIA, LCar = LPA, RutaImg = Ruta });
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