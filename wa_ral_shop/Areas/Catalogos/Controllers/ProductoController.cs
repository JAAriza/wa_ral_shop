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
using wa_ral_shop.Models.Anonymous;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace wa_ral_shop.Areas.Catalogos.Controllers
{
    public class ProductoController : Controller
    {
        public static byte IdUbica = 0;
        public static string Ubica = string.Empty;
        public static int Ide = 0;
        public static List<string> lstDoc = new List<string>();


        // GET: Catalogos/Producto
        
        public ActionResult Producto()
        {
            ActionResult actionResult = null;
            actionResult = View();

            RepositorioProducto repositorioProducto = new RepositorioProducto();
            ContentResultObject contentResultObject = new ContentResultObject();
            List<DataTable> lstCombos = new List<DataTable>();
            DataTable dtCategoria = new DataTable();
            DataTable dtUnidadMedida = new DataTable();
            DataTable dtRuta = new DataTable();
            List<ComboAnonymous> lstCategorias = new List<ComboAnonymous>();
            List<ComboAnonymous> lstUnidadMedida = new List<ComboAnonymous>();
            ComboAnonymous comboAnonymous = new ComboAnonymous();

            try
            {
                lstCombos = repositorioProducto.SelectCombos();
                dtCategoria = lstCombos[0];
                dtUnidadMedida = lstCombos[1];
                dtRuta = lstCombos[2];

                foreach (DataRow dr in dtCategoria.Rows)
                {
                    comboAnonymous = new ComboAnonymous();
                    comboAnonymous.Id = dr[0].ToString();
                    comboAnonymous.Dato = dr[1].ToString();
                    lstCategorias.Add(comboAnonymous);
                }
                foreach (DataRow dr in dtUnidadMedida.Rows)
                {
                    comboAnonymous = new ComboAnonymous();
                    comboAnonymous.Id = dr[0].ToString();
                    comboAnonymous.Dato = dr[1].ToString();
                    lstUnidadMedida.Add(comboAnonymous);
                }
                ViewData["cmbCategorias"] = lstCategorias;
                ViewData["cmbUnidadMedida"] = lstUnidadMedida;
                ViewData["IdRuta"] = dtRuta.Rows[0][0];
                IdUbica = string.IsNullOrEmpty(dtRuta.Rows[0][0].ToString()) ? byte.Parse(0.ToString()) : byte.Parse(dtRuta.Rows[0][0].ToString());
                Info info = new Info();
                ViewData["Ruta"] = info.GetIp() + "Producto" + "\\";
                Ubica = ViewData["Ruta"].ToString();
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
        public ActionResult Alta(byte IdCategoria, string Nombre, string Modelo, string Descripcion,
        float Largo, float Ancho, float Alto, byte IdUMedidaT, float Peso, byte IdUMedidaP,
        string CBarras, decimal Precio)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioProducto repositorioProducto = new RepositorioProducto();
            ProductoAnonymous ProductoAnonymous = new ProductoAnonymous();
            string Mensaje = string.Empty;

            ProductoAnonymous.IdCategoria = IdCategoria;
            ProductoAnonymous.Nombre = Nombre;
            ProductoAnonymous.Modelo = Modelo;
            ProductoAnonymous.Descripcion = Descripcion;
            ProductoAnonymous.Largo = Largo;
            ProductoAnonymous.Alto = Alto;
            ProductoAnonymous.Ancho = Ancho;
            ProductoAnonymous.IdUMedidaT = IdUMedidaT;
            ProductoAnonymous.Peso = Peso;
            ProductoAnonymous.IdUMedidaP = IdUMedidaP;
            ProductoAnonymous.CodBarras = CBarras;
            ProductoAnonymous.PrecioVenta = Precio;
            //ProductoAnonymous.Existencias = Existencias;

            try
            {
                Mensaje = repositorioProducto.Alta(ProductoAnonymous) > 0 ? "Agregado Correctamente" : "Error";
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
        public ActionResult Buscar(string Nombre, string Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioProducto repositorioProducto = new RepositorioProducto();
            ProductoAnonymous ProductoAnonymous = new ProductoAnonymous();
            string Mensaje = string.Empty;
            ProductoAnonymous.Nombre = Nombre;
            ProductoAnonymous.EstatusSTR = Estatus;
            DataTable dtProducto = new DataTable();

            try
            {
                dtProducto = repositorioProducto.Buscar(ProductoAnonymous);
                List<ProductoAnonymous> lstProductoAnonymous = new List<ProductoAnonymous>();
                ProductoAnonymous ProductoA;
                foreach (DataRow dr in dtProducto.Rows)
                {
                    ProductoA = new ProductoAnonymous();
                    ProductoA.categoriaAnonymous = new CategoriaAnonymous();
                    ProductoA.unidadMedidaT = new UnidadMedidaAnonymous();
                    ProductoA.unidadMedidaP = new UnidadMedidaAnonymous();
                    ProductoA.Id = int.Parse(dr["Id"].ToString());
                    ProductoA.IdCategoria = byte.Parse(dr["IdCategoria"].ToString());
                    ProductoA.categoriaAnonymous.Categoria = dr["Categoria"].ToString();
                    ProductoA.Nombre = dr["Nombre"].ToString();
                    ProductoA.Modelo = dr["Modelo"].ToString();
                    ProductoA.Descripcion = dr["Descripcion"].ToString();
                    ProductoA.Largo = float.Parse(dr["Largo"].ToString());
                    ProductoA.Ancho = float.Parse(dr["Ancho"].ToString());
                    ProductoA.Alto = float.Parse(dr["Alto"].ToString());
                    ProductoA.IdUMedidaT = byte.Parse(dr["IdUMedidaT"].ToString());
                    ProductoA.unidadMedidaT.Nombre = dr["UMedidaT"].ToString();
                    ProductoA.Peso = float.Parse(dr["Peso"].ToString());
                    ProductoA.IdUMedidaP = byte.Parse(dr["IdUMedidaP"].ToString());
                    ProductoA.unidadMedidaP.Nombre = dr["UMedidaP"].ToString();
                    ProductoA.CodBarras = dr["CodBarras"].ToString();
                    ProductoA.PrecioVenta = decimal.Parse(dr["PrecioVenta"].ToString());
                    ProductoA.Existencias = string.IsNullOrEmpty(dr["Existencias"].ToString()) ? 0 : int.Parse(dr["Existencias"].ToString());
                    ProductoA.FechaHoraCaptura = DateTime.Parse(dr["FechaHoraCaptura"].ToString());
                    ProductoA.Estatus = Boolean.Parse(dr["Estatus"].ToString());
                    ProductoA.EstatusSTR = Boolean.Parse(dr["Estatus"].ToString()) == true ? "Activo" : "Inactivo";
                    lstProductoAnonymous.Add(ProductoA);
                }

                ViewData["Total"] = lstProductoAnonymous.Count;
                var gridProductos = RenderRazorViewToString(this.ControllerContext, "ListaProductos", lstProductoAnonymous);

                actionResult = Json(new
                {
                    ListaCat = gridProductos
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
        public ActionResult Editar(int Id, byte IdCategoria, string Nombre, string Modelo,
            string Descripcion, float Largo, float Ancho, float Alto, byte IdUMedidaT,
            float Peso, byte IdUMedidaP, string CodBarras, decimal PrecioVenta, bool Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioProducto repositorioProducto = new RepositorioProducto();
            ProductoAnonymous ProductoAnonymous = new ProductoAnonymous();
            string Mensaje = string.Empty;

            ProductoAnonymous.Id = Id;
            ProductoAnonymous.IdCategoria = IdCategoria;
            ProductoAnonymous.Nombre = Nombre;
            ProductoAnonymous.Modelo = Modelo;
            ProductoAnonymous.Descripcion = Descripcion;
            ProductoAnonymous.Largo = Largo;
            ProductoAnonymous.Alto = Alto;
            ProductoAnonymous.Ancho = Ancho;
            ProductoAnonymous.IdUMedidaT = IdUMedidaT;
            ProductoAnonymous.Peso = Peso;
            ProductoAnonymous.IdUMedidaP = IdUMedidaP;
            ProductoAnonymous.CodBarras = CodBarras;
            ProductoAnonymous.PrecioVenta = PrecioVenta;
            ProductoAnonymous.Estatus = Estatus;

            try
            {
                Mensaje = repositorioProducto.Editar(ProductoAnonymous) < 0 ? "Modificado Correctamente" : "Error";
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
        public ActionResult Eliminar(int Id)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioProducto repositorioProducto = new RepositorioProducto();
            string Mensaje = string.Empty;

            try
            {
                Mensaje = repositorioProducto.Eliminar(Id) == -1 ? "Modificado Correctamente" : "Error";
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
        public ActionResult ObtenerImgs(int Id)
        {
            RepositorioProducto repositorioProducto = new RepositorioProducto();
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            DataTable dtImgs = new DataTable();
            List<ComboAnonymous> lstImgs = new List<ComboAnonymous>();
            ComboAnonymous comboAnonymous = new ComboAnonymous();

            try
            {
                dtImgs = repositorioProducto.GetImagenes(Id);
                foreach (DataRow dr in dtImgs.Rows)
                {
                    comboAnonymous = new ComboAnonymous();
                    comboAnonymous.Id = dr[0].ToString();
                    comboAnonymous.Dato = dr[1].ToString();
                    lstImgs.Add(comboAnonymous);
                }
                actionResult = Json(new { limgs = lstImgs });
                Ide = Id;
            }
            catch (Exception ex)
            {
                Ide = 0;
                IdUbica = 0;
                Ubica = string.Empty;
                ContentResultObject.Codigo = "Error";
                ContentResultObject.Mensaje = ex.Message;
                actionResult = Json(new { codigo = ContentResultObject.Codigo, mensaje = ContentResultObject.Mensaje });
            }
            return actionResult;
        }

        [HttpPost]
        public ActionResult CrearCarpeta(int IdProducto, byte IdRuta, string Ruta)
        {
            Ide = IdProducto;
            IdUbica = IdRuta;
            Ubica = Ruta;
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;

            string Ubicacion = Ide.ToString();

            string RutaCompleta = Path.Combine(Ruta, Ubicacion);

            try
            {
                if (Directory.Exists(RutaCompleta))
                {
                    actionResult = Json(new { mensaje = "La Ruta ha sido creada" });
                }
                else
                {
                    string[] RutaD = RutaCompleta.Split('\\');
                    string PartialRuta = "";
                    for (int i = 1; i < RutaD.Length; i++)
                    {
                        PartialRuta = "";
                        for (int j = 0; j <= i; j++)
                        {
                            PartialRuta = PartialRuta + RutaD[j] + '\\';
                        }
                        if (!Directory.Exists(PartialRuta))
                        {
                            Directory.CreateDirectory(PartialRuta);
                        }

                    }
                    actionResult = Json(new { mensaje = "Ruta Creada" });
                }
            }
            catch (Exception ex)
            {
                Ide = 0;
                IdUbica = 0;
                Ubica = string.Empty;
                ContentResultObject.Codigo = "Error";
                ContentResultObject.Mensaje = ex.Message;
                actionResult = Json(new { codigo = ContentResultObject.Codigo, mensaje = ContentResultObject.Mensaje });
            }
            return actionResult;
        }

        [HttpPost]
        public ActionResult AgregarArchivo()
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            List<HttpPostedFileBase> listaArchivos = new List<HttpPostedFileBase>();

            if (Ide == 0 && IdUbica == 0 && string.IsNullOrEmpty(Ubica))
            {
                listaArchivos.Clear();
                ContentResultObject.Codigo = "Error";
                ContentResultObject.Mensaje = "No se pudo Obtener toda la informacion";
                actionResult = Json(new { codigo = ContentResultObject.Codigo, mensaje = ContentResultObject.Mensaje });
                return actionResult;
            }

            string Mensaje = "";
            List<string> lstImgV = new List<string>();
            string Ubicacion = Ide.ToString();
            int Ultimo = 0;
            int Exist = 0;
            string RutaCompleta = Path.Combine(Ubica, Ubicacion);

            //double tamanoTotal = 0;
            try
            {
                HttpFileCollectionBase archivos = Request.Files;
                if (archivos.Count > 0)
                {
                    for (int i = 0; i < archivos.Count; i++)
                    {
                        HttpPostedFileBase imagen = archivos[i];
                        //tamanoTotal += imagen.ContentLength;
                        listaArchivos.Add(imagen);
                    }
                    //tamanoTotal = (tamanoTotal / 1e+6);
                    //if (tamanoTotal >= 20)
                    //{
                    //    actionResult = Json(new { mensaje = "El tamaño total de las imagenes supera el permitido.", uploaded = false });
                    //    listaArchivos.Clear();
                    //}
                    //else
                    //{
                    //    actionResult = Json(new { mensaje = "Imagenes cargadas correctamente.", uploaded = true });
                    //}
                }
                else
                {
                    actionResult = Json(new { mensaje = "No se adjuntaron archivos.", uploaded = true });
                }

                if (listaArchivos.Count > 0)
                {
                    if (Directory.Exists(RutaCompleta))
                    {
                        var files = from file in
                                        Directory.EnumerateFiles(RutaCompleta)
                                    select file;
                        int consecutivo = 0;
                        consecutivo = files.Count();
                        _ = consecutivo == 0 ? consecutivo = 1 : consecutivo++;
                        Exist = consecutivo;
                        for (int i = 0; i < listaArchivos.Count; i++)
                        {
                            string Nombre = "ImgV" + "-" + Ide + "-" + consecutivo++;
                            var fileName = Path.GetFileName(listaArchivos[i].FileName);
                            string ACompleto = Path.Combine(RutaCompleta, Nombre + Path.GetExtension(listaArchivos[i].FileName));
                            listaArchivos[i].SaveAs(ACompleto);
                            lstDoc.Add(Nombre + Path.GetExtension(listaArchivos[i].FileName));
                        }
                        Mensaje = "Archivos almacenados";
                        lstImgV = lstDoc;
                        var filess = from file in
                                        Directory.EnumerateFiles(RutaCompleta)
                                     select file;

                        Ultimo = files.Count();
                    }
                    else
                    {
                        string[] RutaD = RutaCompleta.Split('\\');
                        string PartialRuta = "";
                        for (int i = 1; i < RutaD.Length; i++)
                        {
                            PartialRuta = "";
                            for (int j = 0; j <= i; j++)
                            {
                                PartialRuta = PartialRuta + RutaD[j] + '\\';
                            }
                            if (!Directory.Exists(PartialRuta))
                            {
                                Directory.CreateDirectory(PartialRuta);
                            }
                        }

                        if (listaArchivos.Count > 0)
                        {
                            var files = from file in
                                        Directory.EnumerateFiles(RutaCompleta)
                                        select file;
                            int consecutivo = 0;
                            consecutivo = files.Count();
                            _ = consecutivo == 0 ? consecutivo = 1 : consecutivo++;
                            Exist = consecutivo;
                            for (int i = 0; i < listaArchivos.Count; i++)
                            {
                                string Nombre = "ImgV" + "-" + Ide + "-" + consecutivo++;
                                var fileName = Path.GetFileName(listaArchivos[i].FileName);
                                string ACompleto = Path.Combine(RutaCompleta, Nombre + Path.GetExtension(listaArchivos[i].FileName));
                                listaArchivos[i].SaveAs(ACompleto);
                                lstDoc.Add(Nombre + Path.GetExtension(listaArchivos[i].FileName));
                            }
                            Mensaje = "Archivos almacenados";
                            lstImgV = lstDoc;
                            var filess = from file in
                                        Directory.EnumerateFiles(RutaCompleta)
                                         select file;

                            Ultimo = files.Count();
                        }
                        else
                        {
                            actionResult = Json(new { mensaje = "No se pudo procesar el(los) archivo(s).", uploaded = true });
                        }
                    }
                    actionResult = Json(new { mensaje = Mensaje, lstFile = lstImgV, final = Ultimo, primero = Exist });
                }
                else
                {
                    ContentResultObject.Codigo = "Error";
                    Mensaje = "No se cargaron los archivos";
                    actionResult = Json(new { codigo = ContentResultObject.Codigo, mensaje = Mensaje });
                }
            }
            catch (Exception ex)
            {
                ContentResultObject.Codigo = "Error";
                ContentResultObject.Mensaje = ex.Message;
                actionResult = Json(new { codigo = ContentResultObject.Codigo, mensaje = ContentResultObject.Mensaje });
            }

            //Ide = 0;
            //IdUbica = 0;
            //Ubica = string.Empty;
            lstDoc = new List<string>();
            listaArchivos.Clear();

            return actionResult;
        }

        [HttpPost]
        //[ValidateInput(false)]
        public JsonResult GuardarTabla(ComboAnonymous[] comboAnonymous)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            JsonResult actionResult = null;
            //DataSet ds = new DataSet();
            RepositorioProducto repositorioProducto = new RepositorioProducto();
            int del;

            try
            {
                if (Ide == 0 && IdUbica == 0)// && string.IsNullOrEmpty(Ubica))
                {
                    ContentResultObject.Codigo = "Error";
                    ContentResultObject.Mensaje = "No se pudo Obtener toda la informacion";
                    actionResult = Json(new { codigo = ContentResultObject.Codigo, mensaje = ContentResultObject.Mensaje });
                    return actionResult;
                }

                DataTable dtImg = new DataTable();
                dtImg.Clear();
                dtImg.Columns.Add("IdProducto", typeof(int));
                dtImg.Columns.Add("IdRutaBase", typeof(byte));
                //dtImg.Columns.Add("Ubicacion", typeof(string));
                dtImg.Columns.Add("Nombre", typeof(string));
                dtImg.Columns.Add("Extension", typeof(string));
                dtImg.Columns.Add("Consecutivo", typeof(byte));

                foreach (ComboAnonymous combo in comboAnonymous)
                {
                    DataRow dr = dtImg.NewRow();
                    string[] NomExt;
                    dr["IdProducto"] = Ide;
                    dr["IdRutaBase"] = IdUbica;
                    //dr["Ubicacion"] = Ubica;
                    NomExt = combo.Id.Split('.');
                    dr["Nombre"] = combo.Id;
                    dr["Extension"] = NomExt[NomExt.Length - 1];
                    dr["Consecutivo"] = combo.Dato;
                    dtImg.Rows.Add(dr);
                }
                del = repositorioProducto.EliminarImgProd(Ide);
                if (del == -1)
                {
                    repositorioProducto.GuardarMedia(dtImg);
                }
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
        public ActionResult EliminarMedia(string UbiC)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            string Mensaje = string.Empty;

            try
            {
                System.IO.File.Delete(UbiC);
                Mensaje = "Eliminado";
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

        
        //[HttpPost]
        //public ActionResult GuardarD(Int16 Id, Int16 IdDocumento, string Documento,
        //    byte IdRuta, string Ruta)
        //{
        //    ContentResultObject ContentResultObject = new ContentResultObject();
        //    ActionResult actionResult = null;
        //    RepositorioColaborador repositorioColaborador = new RepositorioColaborador();
        //    DColaboradorAnonymous dColaboradorAnonymous;
        //    List<DColaboradorAnonymous> lstdColaborador;

        //    string Mensaje = string.Empty;

        //    string Ubicacion = DateTime.Now.ToString("yyyy")
        //        + "\\" + DateTime.Now.ToString("MM")
        //        + "\\" + DateTime.Now.ToString("dd")
        //        + "\\" + Id;

        //    try
        //    {
        //        List<string> lstArchivos = new List<string>();
        //        foreach (var doc in lstDoc)
        //        {
        //            lstArchivos.Add(doc);
        //        }
        //        lstDoc.Clear();

        //        lstdColaborador = new List<DColaboradorAnonymous>();
        //        foreach (var archivo in lstArchivos)
        //        {
        //            string[] nomCompleto = null;
        //            nomCompleto = archivo.Split('.');
        //            dColaboradorAnonymous = new DColaboradorAnonymous();
        //            dColaboradorAnonymous.IdColaborador = Id;
        //            dColaboradorAnonymous.IdDocumento = IdDocumento;
        //            dColaboradorAnonymous.Ubicacion = Ubicacion;
        //            dColaboradorAnonymous.Nombre = nomCompleto[0];
        //            dColaboradorAnonymous.Extension = nomCompleto[1];
        //            dColaboradorAnonymous.IdRuta = IdRuta;
        //            lstdColaborador.Add(dColaboradorAnonymous);
        //        }
        //        DataTable dtDColaborador = ConvertToDataTable(lstdColaborador);

        //        Mensaje = repositorioColaborador.GuardarDocumentos(dtDColaborador) < 0 ? "Agregado Correctamente" : "Error";
        //        actionResult = Json(new { mensaje = Mensaje });
        //    }
        //    catch (Exception Ex)
        //    {
        //        ContentResultObject.Codigo = "Error";
        //        ContentResultObject.Mensaje = Ex.Message;
        //        actionResult = Json(new { codigo = ContentResultObject.Codigo, mensaje = ContentResultObject.Mensaje });
        //    }
        //    return actionResult;
        //}

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

        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }


        /*Princpal
         Parte de codigo a utilizar en el dashboard para consultar y mostrar los productos
        
         Recuerda que tiene que tienes que considerar paginar ademas de usar ajax y no saturar
        la bd con tanta consulta
        igual para hacer el actualizar por partes*/

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult ConsultarProductosActivos()
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            string Mensaje = string.Empty;
            DataTable dtProductosDash = new DataTable();
            RepositorioProducto repositorioProducto = new RepositorioProducto();
            List<ProductoImagenAnonymous> LPIA = new List<ProductoImagenAnonymous>();
            List<ProductoAnonymous> LPA = new List<ProductoAnonymous>();
            try
            {
                dtProductosDash = repositorioProducto.GetProductosActivosDashboard();
                if (dtProductosDash.Rows.Count > 0)
                {
                    DataTable dtProd = new DataTable();
                    dtProd = dtProductosDash.DefaultView.ToTable(true, "Id", "NomP", "Existencias", "PrecioVenta");
                    
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
                actionResult = Json(new {LPI = LPIA, LProd = LPA, RutaImg = Ruta});
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
        public ActionResult ConsultarProductoDetalle(int IdProducto)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            string Mensaje = string.Empty;
            DataTable dtProductosDash = new DataTable();
            RepositorioProducto repositorioProducto = new RepositorioProducto();
            List<ProductoImagenAnonymous> LPIA = new List<ProductoImagenAnonymous>();
            List<ProductoImagenAnonymous> newLPIA = new List<ProductoImagenAnonymous>();
            List<ProductoAnonymous> LPA = new List<ProductoAnonymous>();
            try
            {
                dtProductosDash = repositorioProducto.GetProductoDetalle(IdProducto);
                if (dtProductosDash.Rows.Count > 0)
                {
                    DataTable dtProd = new DataTable();
                    dtProd = dtProductosDash.DefaultView.ToTable(
                        true, "Id", "NomP", "Modelo", "Descripcion"
                        , "Largo", "Ancho", "Alto", "NomUMT"
                        , "Peso", "NomUMP"
                        , "PrecioVenta", "Existencias");

                    LPIA = new List<ProductoImagenAnonymous>();
                    foreach (DataRow dr in dtProductosDash.Rows)
                    {
                        ProductoImagenAnonymous PIA = new ProductoImagenAnonymous();
                        PIA.IdProducto = int.Parse(dr[0].ToString());
                        PIA.Nombre = dr["NomI"].ToString();
                        //PIA.Extension = dr["Extension"].ToString();
                        PIA.Consecutivo = byte.Parse(dr["Consecutivo"].ToString());
                        LPIA.Add(PIA);
                    }
                    newLPIA = LPIA.OrderBy(x => x.Nombre)
                        .ThenBy(x => x.Consecutivo)
                        .ToList();

                    LPA = new List<ProductoAnonymous>();
                    foreach (DataRow dr in dtProd.Rows)
                    {
                        ProductoAnonymous PA = new ProductoAnonymous();
                        PA.unidadMedidaT = new UnidadMedidaAnonymous();
                        PA.unidadMedidaP = new UnidadMedidaAnonymous();
                        PA.Id = int.Parse(dr[0].ToString());
                        PA.Nombre = dr["NomP"].ToString();
                        PA.Modelo = dr["Modelo"].ToString();
                        PA.Descripcion = dr["Descripcion"].ToString();
                        PA.Largo = float.Parse(dr["Largo"].ToString());
                        PA.Ancho = float.Parse(dr["Ancho"].ToString());
                        PA.Alto = float.Parse(dr["Alto"].ToString());
                        PA.unidadMedidaT.Nombre = dr["NomUMT"].ToString();
                        PA.Peso = float.Parse(dr["Peso"].ToString());
                        PA.unidadMedidaP.Nombre = dr["NomUMP"].ToString();
                        PA.PrecioVenta = decimal.Parse(dr["PrecioVenta"].ToString());
                        PA.Existencias = string.IsNullOrEmpty(dr["Existencias"].ToString()) ? 0 : int.Parse(dr["Existencias"].ToString());
                        
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
                actionResult = Json(new { LPI = newLPIA, LProd = LPA, RutaImg = Ruta });
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