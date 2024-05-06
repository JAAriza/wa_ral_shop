using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using wa_ral_shop.Models.Utilerias;
using wa_ral_shop.Models.Repositorios.Administracion;
using wa_ral_shop.Models.Anonymous.Administracion;
using System.Data;
using System.IO;
using wa_ral_shop.Models.Anonymous;
using System.ComponentModel;

namespace wa_ral_shop.Areas.Administracion.Controllers
{
    public class CompraController : Controller
    {
        // GET: Administracion/Compra
        public ActionResult Compra()
        {
            ActionResult actionResult = null;
            actionResult = View();

            RepositorioCompra repositorioCompra = new RepositorioCompra();
            ContentResultObject contentResultObject = new ContentResultObject();
            List<DataTable> lstCombos = new List<DataTable>();
            DataTable dtProducto = new DataTable();
            DataTable dtProveedor = new DataTable();
            List<ComboAnonymous> lstProductos = new List<ComboAnonymous>();
            List<ComboAnonymous> lstProveedores = new List<ComboAnonymous>();
            ComboAnonymous comboAnonymous = new ComboAnonymous();

            try
            {
                lstCombos = repositorioCompra.SelectCombos();
                dtProducto = lstCombos[0];
                dtProveedor = lstCombos[1];

                foreach (DataRow dr in dtProducto.Rows)
                {
                    comboAnonymous = new ComboAnonymous();
                    comboAnonymous.Id = dr[0].ToString();
                    comboAnonymous.Dato = dr[1].ToString();
                    lstProductos.Add(comboAnonymous);
                }
                foreach (DataRow dr in dtProveedor.Rows)
                {
                    comboAnonymous = new ComboAnonymous();
                    comboAnonymous.Id = dr[0].ToString();
                    comboAnonymous.Dato = dr[1].ToString();
                    lstProveedores.Add(comboAnonymous);
                }
                ViewData["cmbProducto"] = lstProductos;
                ViewData["cmbProveedor"] = lstProveedores;
            }
            catch (Exception Ex)
            {
                contentResultObject.Codigo = "Error";
                contentResultObject.Mensaje = Ex.Message;
                actionResult = Json(new { codigo = contentResultObject.Codigo, mensaje = contentResultObject.Mensaje });
            }

            return actionResult;
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Buscar(string FI, string FF, string Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCompra repositorioCompra = new RepositorioCompra();
            string Mensaje = string.Empty;
            DataTable dtCompra = new DataTable();
            DateTime FecIn;
            DateTime FecFin;
            try
            {
                if (string.IsNullOrEmpty(FI))
                {
                    FecIn = DateTime.Parse("1/1/1753");
                }
                else
                {
                    FecIn = DateTime.Parse(FI);
                }
                
                if (string.IsNullOrEmpty(FF))
                {
                    FecFin = DateTime.Parse("1/1/1753");
                }
                else
                {
                    FecFin = DateTime.Parse(FF);
                }

                dtCompra = repositorioCompra.Buscar(FecIn, FecFin, Estatus);
                List<CompraAnonymous> lstCompraAnonymous = new List<CompraAnonymous>();
                CompraAnonymous CompraA;
                foreach (DataRow dr in dtCompra.Rows)
                {
                    CompraA = new CompraAnonymous();
                    CompraA.Id = string.IsNullOrEmpty(dr["Id"].ToString()) ? 0 : Convert.ToInt32(dr["Id"].ToString());
                    CompraA.CostoEnvio = string.IsNullOrEmpty(dr["CostoEnvio"].ToString()) ? decimal.Zero : Convert.ToDecimal(dr["CostoEnvio"].ToString());
                    CompraA.FechaHoraAvisoImportacion = string.IsNullOrEmpty(dr["FechaHoraAvisoImportacion"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(dr["FechaHoraAvisoImportacion"].ToString());
                    CompraA.FechaHoraLlegadaMX = string.IsNullOrEmpty(dr["FechaHoraLlegadaMX"].ToString()) ? DateTime.MinValue : Convert.ToDateTime(dr["FechaHoraLLegadaMX"].ToString());
                    CompraA.CostoTotal = string.IsNullOrEmpty(dr["CostoTotal"].ToString()) ? decimal.Zero : Convert.ToDecimal(dr["CostoTotal"].ToString());
                    CompraA.Estatus = string.IsNullOrEmpty(dr["Estatus"].ToString()) ? string.Empty : dr["Estatus"].ToString();
                    CompraA.Validado = string.IsNullOrEmpty(dr["Validado"].ToString()) ? false : Boolean.Parse(dr["Validado"].ToString()); 
                    lstCompraAnonymous.Add(CompraA);
                }

                ViewData["Total"] = lstCompraAnonymous.Count;
                var gridCompras = RenderRazorViewToString(this.ControllerContext, "ListaCompras", lstCompraAnonymous);

                actionResult = Json(new
                {
                    ListaCat = gridCompras
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
        public ActionResult Alta(string Envio, DateTime Aviso, DateTime Llegada, string Total, string Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCompra repositorioCompra = new RepositorioCompra();
            CompraAnonymous compraAnonymous = new CompraAnonymous();
            string Mensaje = string.Empty;
            int IdCompra;

            compraAnonymous.CostoEnvio = decimal.Parse(Envio.Split('$').Last());
            compraAnonymous.FechaHoraAvisoImportacion = Aviso;
            compraAnonymous.FechaHoraLlegadaMX = Llegada;
            compraAnonymous.CostoTotal = decimal.Parse(Total.Split('$').Last());
            compraAnonymous.Estatus = Estatus;

            try
            {
                IdCompra = repositorioCompra.Alta(compraAnonymous);
                Mensaje = IdCompra > 0 ? "Agregado Correctamente" : "Error";
                actionResult = Json(new { mensaje = Mensaje, idCompra = IdCompra });
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
        public ActionResult AltaDetalle(List<CompraDetalleAnonymous> compraD)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCompra repositorioCompra = new RepositorioCompra();
            string Mensaje = string.Empty;
            
            try
            {
                DataTable dtCompraD = new DataTable();
                dtCompraD.Clear();
                dtCompraD.Columns.Add("IdCompra", typeof(int));
                dtCompraD.Columns.Add("IdProducto", typeof(int));
                dtCompraD.Columns.Add("Cantidad", typeof(int));
                dtCompraD.Columns.Add("IdProveedor", typeof(int));
                dtCompraD.Columns.Add("CostoUnitario", typeof(decimal));
                dtCompraD.Columns.Add("CostoEnvio", typeof(decimal));
                dtCompraD.Columns.Add("FechaHoraCompra", typeof(DateTime));
                dtCompraD.Columns.Add("FechaHoraLlegadaUSA", typeof(DateTime));

                foreach (CompraDetalleAnonymous compraDet in compraD)
                {
                    DataRow dr = dtCompraD.NewRow();
                    dr["IdCompra"] = compraDet.IdCompra;
                    dr["IdProducto"] = compraDet.IdProducto;
                    dr["Cantidad"] = compraDet.Cantidad;
                    dr["IdProveedor"] = compraDet.IdProveedor;
                    dr["CostoUnitario"] = compraDet.CostoUnitario;
                    dr["CostoEnvio"] = compraDet.CostoEnvio;
                    dr["FechaHoraCompra"] = compraDet.FechaHoraCompra;
                    dr["FechaHoraLlegadaUSA"] = compraDet.FechaHoraLlegadaUSA;
                    dtCompraD.Rows.Add(dr);
                }

                Mensaje = repositorioCompra.GuardarDetalle(dtCompraD) < 0 ? "Agregado Correctamente" : "Error";
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
        public JsonResult FalloGuardarDet(int IdCompra)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            JsonResult actionResult = null;
            RepositorioCompra repositorioCompra = new RepositorioCompra();
            string Mensaje = "";

            try
            {
                Mensaje = repositorioCompra.EliminarCompra(IdCompra) < 0 ? "Eliminado Correctamente" : "Error";              
            }
            catch (Exception ex)
            {
                ContentResultObject.Codigo = "Error";
                ContentResultObject.Mensaje = ex.Message;
                actionResult = Json(new { codigo = ContentResultObject.Codigo, mensaje = ContentResultObject.Mensaje });
            }
            return actionResult;
        }

        [HttpPost]
        public ActionResult ObtenerDetalle(int Id)
        {
            RepositorioCompra repositorioCompra = new RepositorioCompra();
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            DataTable dtDet = new DataTable();
            List<CompraDetalleAnonymous> lstDet = new List<CompraDetalleAnonymous>();
            CompraDetalleAnonymous compraDetalleAnonymous;

            try
            {
                dtDet = repositorioCompra.GetDetalle(Id);
                foreach (DataRow dr in dtDet.Rows)
                {
                    compraDetalleAnonymous = new CompraDetalleAnonymous();
                    compraDetalleAnonymous.ProductoA = new Models.Anonymous.Catalogos.ProductoAnonymous();
                    compraDetalleAnonymous.ProveedorA = new Models.Anonymous.Catalogos.ProveedorAnonymous();

                    compraDetalleAnonymous.IdProducto = int.Parse(dr["IdProducto"].ToString());
                    compraDetalleAnonymous.ProductoA.Nombre = dr["Producto"].ToString();
                    compraDetalleAnonymous.Cantidad = int.Parse(dr["Cantidad"].ToString());
                    compraDetalleAnonymous.IdProveedor = int.Parse(dr["IdProveedor"].ToString());
                    compraDetalleAnonymous.ProveedorA.Nombre = dr["Proveedor"].ToString();
                    compraDetalleAnonymous.CostoUnitario = decimal.Parse(dr["CostoUnitario"].ToString());
                    compraDetalleAnonymous.CostoEnvio = decimal.Parse(dr["CostoEnvio"].ToString());
                    compraDetalleAnonymous.FechaHoraCompra = Convert.ToDateTime(dr["FechaHoraCompra"].ToString());
                    compraDetalleAnonymous.FechaHoraLlegadaUSA = Convert.ToDateTime(dr["FechaHoraLlegadaUSA"].ToString());

                    lstDet.Add(compraDetalleAnonymous);
                }
                actionResult = Json(new { ldet = lstDet });

            }
            catch (Exception ex)
            {
                ContentResultObject.Codigo = "Error";
                ContentResultObject.Mensaje = ex.Message;
                actionResult = Json(new { codigo = ContentResultObject.Codigo, mensaje = ContentResultObject.Mensaje });
            }
            return actionResult;
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Editar(int IdCompra, decimal Envio, DateTime Aviso, DateTime Llegada
            , decimal Total, string Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCompra repositorioCompra = new RepositorioCompra();
            CompraAnonymous compraAnonymous = new CompraAnonymous();
            string Mensaje = string.Empty;

            compraAnonymous.Id = IdCompra;
            compraAnonymous.CostoEnvio = Envio;
            compraAnonymous.FechaHoraAvisoImportacion = Aviso;
            compraAnonymous.FechaHoraLlegadaMX = Llegada;
            compraAnonymous.CostoTotal = Total;
            compraAnonymous.Estatus = Estatus;

            try
            {
                Mensaje = repositorioCompra.Editar(compraAnonymous) < 0 ? "Modificado Correctamente" : "Error";
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
        public ActionResult EditarDetalle(List<CompraDetalleAnonymous> compraD)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioCompra repositorioCompra = new RepositorioCompra();
            string Mensaje = string.Empty;

            try
            {
                DataTable dtCompraD = new DataTable();
                dtCompraD.Clear();
                dtCompraD.Columns.Add("IdCompra", typeof(int));
                dtCompraD.Columns.Add("IdProducto", typeof(int));
                dtCompraD.Columns.Add("Cantidad", typeof(int));
                dtCompraD.Columns.Add("IdProveedor", typeof(int));
                dtCompraD.Columns.Add("CostoUnitario", typeof(decimal));
                dtCompraD.Columns.Add("CostoEnvio", typeof(decimal));
                dtCompraD.Columns.Add("FechaHoraCompra", typeof(DateTime));
                dtCompraD.Columns.Add("FechaHoraLlegadaUSA", typeof(DateTime));

                foreach (CompraDetalleAnonymous compraDet in compraD)
                {
                    DataRow dr = dtCompraD.NewRow();
                    dr["IdCompra"] = compraDet.IdCompra;
                    dr["IdProducto"] = compraDet.IdProducto;
                    dr["Cantidad"] = compraDet.Cantidad;
                    dr["IdProveedor"] = compraDet.IdProveedor;
                    dr["CostoUnitario"] = compraDet.CostoUnitario;
                    dr["CostoEnvio"] = compraDet.CostoEnvio;
                    dr["FechaHoraCompra"] = compraDet.FechaHoraCompra;
                    dr["FechaHoraLlegadaUSA"] = compraDet.FechaHoraLlegadaUSA;
                    dtCompraD.Rows.Add(dr);
                }

                Mensaje = repositorioCompra.EditarDetalle(dtCompraD) < 0 ? "Agregado Correctamente" : "Error";
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
        public JsonResult FalloGuardarDetEditar(int IdCompra)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            JsonResult actionResult = null;
            RepositorioCompra repositorioCompra = new RepositorioCompra();
            string Mensaje = "";

            try
            {
                Mensaje = repositorioCompra.EliminarCompraEditar(IdCompra) < 0 ? "Eliminado Correctamente" : "Error";
            }
            catch (Exception ex)
            {
                ContentResultObject.Codigo = "Error";
                ContentResultObject.Mensaje = ex.Message;
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