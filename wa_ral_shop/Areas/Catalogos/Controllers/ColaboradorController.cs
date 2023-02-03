using System;
using System.Collections.Generic;

using System.Web.Mvc;
using wa_ral_shop.Models.Utilerias;
using wa_ral_shop.Models.Repositorios.Catalogos;
using wa_ral_shop.Models.Repositorios.Administracion;
using wa_ral_shop.Models.Anonymous;
using wa_ral_shop.Models.Anonymous.Catalogos;
using wa_ral_shop.Models.Anonymous.Administracion;
using System.Data;
using System.IO;
using System.Web;
using System.Threading.Tasks;
using System.ComponentModel;

namespace wa_ral_shop.Areas.Catalogos.Controllers
{
    public class ColaboradorController : Controller
    {

        public static string Docto = string.Empty;
        public static string Ubica = string.Empty;
        public static Int16 Ide = 0;
        public static List<string> lstDoc = new List<string>();

        // GET: Catalogos/Colaborador
        public ActionResult Colaborador()
        {
            ActionResult actionResult = null;
            actionResult = View();

            RepositorioColaborador repositorioColaborador = new RepositorioColaborador();
            ContentResultObject contentResultObject = new ContentResultObject();
            List<DataTable> lstCombos = new List<DataTable>();
            DataTable dtPuestos = new DataTable();
            DataTable dtDocumentos = new DataTable();
            DataTable dtRuta = new DataTable();
            List<ComboAnonymous> lstPuestos = new List<ComboAnonymous>();
            List<ComboAnonymous> lstDocumentos = new List<ComboAnonymous>();
            ComboAnonymous comboAnonymous = new ComboAnonymous();

            try
            {
                lstCombos = repositorioColaborador.SelectCombos();
                dtPuestos = lstCombos[0];
                dtDocumentos = lstCombos[1];
                dtRuta = lstCombos[2];
                foreach (DataRow dr in dtPuestos.Rows)
                {
                    comboAnonymous = new ComboAnonymous();
                    comboAnonymous.Id = dr[0].ToString();
                    comboAnonymous.Dato = dr[1].ToString();
                    lstPuestos.Add(comboAnonymous);
                }
                foreach (DataRow dr in dtDocumentos.Rows)
                {
                    comboAnonymous = new ComboAnonymous();
                    comboAnonymous.Id = dr[0].ToString();
                    comboAnonymous.Dato = dr[1].ToString();
                    lstDocumentos.Add(comboAnonymous);
                }
                ViewData["cmbPuestos"] = lstPuestos;
                ViewData["cmbDocumentos"] = lstDocumentos;
                ViewData["IdRuta"] = dtRuta.Rows[0][0];
                ViewData["Ruta"] = dtRuta.Rows[0][1];
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
        public ActionResult Alta(Int16 IdPuesto, string Nombre, string APaterno, string AMaterno
            , string Telefono, string EMail, DateTime FHCon)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioColaborador repositorioColaborador = new RepositorioColaborador();
            ColaboradorAnonymous ColaboradorAnonymous = new ColaboradorAnonymous();
            string Mensaje = string.Empty;
            string EMailEnc;
            string PassEnc = string.Empty;
            int Id = 0;

            EMailEnc = EncYDec.EncryptPlainTextToCipherText(EMail);
            PassEnc = EncYDec.EncryptPlainTextToCipherText("123");

            ColaboradorAnonymous.IdPuesto = IdPuesto;
            ColaboradorAnonymous.Nombre = Nombre;
            ColaboradorAnonymous.APaterno = APaterno;
            ColaboradorAnonymous.AMaterno = AMaterno;
            ColaboradorAnonymous.Telefono = Telefono;
            ColaboradorAnonymous.EMail = EMail;
            ColaboradorAnonymous.FechaHoraContratacion = FHCon;

            try
            {
                Id = repositorioColaborador.Alta(ColaboradorAnonymous);
                if (Id >0)
                {
                    if (repositorioColaborador.AltaU(Id, PassEnc, EMailEnc) > 0)
                    {
                        Mensaje = "Agregado Correctamente";
                    }
                    else
                    {
                        repositorioColaborador.EliminarFalloU(Id);
                        Mensaje = "Error";
                    }
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

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Buscar(string Nombre, Int16 IdPuesto, string Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioColaborador repositorioColaborador = new RepositorioColaborador();
            ColaboradorAnonymous ColaboradorAnonymous = new ColaboradorAnonymous();
            string Mensaje = string.Empty;
            ColaboradorAnonymous.Nombre = Nombre;
            ColaboradorAnonymous.IdPuesto = IdPuesto;
            ColaboradorAnonymous.EstatusSTR = Estatus;
            DataTable dtColaborador = new DataTable();

            try
            {
                dtColaborador = repositorioColaborador.Buscar(ColaboradorAnonymous);
                List<ColaboradorAnonymous> lstColaboradorAnonymous = new List<ColaboradorAnonymous>();
                ColaboradorAnonymous ColaboradorA;
                foreach (DataRow dr in dtColaborador.Rows)
                {
                    ColaboradorA = new ColaboradorAnonymous();
                    ColaboradorA.PuestoAnonymous = new PuestoAnonymous();
                    ColaboradorA.Id = Int16.Parse(dr["Id"].ToString());
                    ColaboradorA.IdPuesto = Int16.Parse(dr["IdPuesto"].ToString());
                    ColaboradorA.PuestoAnonymous.Puesto = dr["Puesto"].ToString();
                    ColaboradorA.Nombre = dr["Nombre"].ToString();
                    ColaboradorA.APaterno = dr["APaterno"].ToString();
                    ColaboradorA.AMaterno = dr["AMaterno"].ToString();
                    ColaboradorA.Telefono = dr["Telefono"].ToString();
                    ColaboradorA.EMail = dr["EMail"].ToString();
                    ColaboradorA.FechaHoraContratacion = DateTime.Parse(dr["FechaHoraContratacion"].ToString());
                    ColaboradorA.FechaHoraCaptura = DateTime.Parse(dr["FechaHoraCaptura"].ToString());
                    ColaboradorA.Estatus = Boolean.Parse(dr["Estatus"].ToString());
                    ColaboradorA.EstatusSTR = Boolean.Parse(dr["Estatus"].ToString()) == true ? "Activo" : "Inactivo";
                    lstColaboradorAnonymous.Add(ColaboradorA);
                }

                ViewData["Total"] = lstColaboradorAnonymous.Count;
                var gridColaboradores = RenderRazorViewToString(this.ControllerContext, "ListaColaborador", lstColaboradorAnonymous);

                actionResult = Json(new
                {
                    ListaCat = gridColaboradores
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
        public ActionResult Editar(Int16 Id, Int16 IdPuesto, string Nombre, string APaterno, string AMaterno
            , string Telefono, string EMail, DateTime FHCon, bool Estatus)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioColaborador repositorioColaborador = new RepositorioColaborador();
            ColaboradorAnonymous ColaboradorAnonymous = new ColaboradorAnonymous();
            string Mensaje = string.Empty;
            ColaboradorAnonymous.Id = Id;
            ColaboradorAnonymous.IdPuesto = IdPuesto;
            ColaboradorAnonymous.Nombre = Nombre;
            ColaboradorAnonymous.APaterno = APaterno;
            ColaboradorAnonymous.AMaterno = AMaterno;
            ColaboradorAnonymous.Telefono = Telefono;
            ColaboradorAnonymous.EMail = EMail;
            ColaboradorAnonymous.FechaHoraContratacion = FHCon;
            ColaboradorAnonymous.Estatus = Estatus;
            try
            {
                Mensaje = repositorioColaborador.Editar(ColaboradorAnonymous) < 0 ? "Modificado Correctamente" : "Error";
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
            RepositorioColaborador repositorioColaborador = new RepositorioColaborador();
            string Mensaje = string.Empty;

            try
            {
                Mensaje = repositorioColaborador.Eliminar(Id) == -1 ? "Modificado Correctamente" : "Error";
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
        public ActionResult CrearCarpeta(Int16 Id, string Documento, string Ruta)
        {
            Ide = Id;
            Docto = Documento;
            Ubica = Ruta;
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;

            string Ubicacion = DateTime.Now.ToString("yyyy")
                + "\\" + DateTime.Now.ToString("MM")
                + "\\" + DateTime.Now.ToString("dd")
                + "\\" + Id;

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
                Docto = string.Empty;
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

            if (Ide == 0 && Docto == string.Empty && Ubica == string.Empty)
            {
                listaArchivos.Clear();
                ContentResultObject.Codigo = "Error";
                ContentResultObject.Mensaje = "No se pudo Obtener toda la informacion";
                actionResult = Json(new { codigo = ContentResultObject.Codigo, mensaje = ContentResultObject.Mensaje });
                return actionResult;
            }

            string Mensaje = "";
            string Ubicacion = DateTime.Now.ToString("yyyy")
                + "\\" + DateTime.Now.ToString("MM")
                + "\\" + DateTime.Now.ToString("dd")
                + "\\" + Ide;

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
                        for (int i = 0; i < listaArchivos.Count; i++)
                        {
                            string Nombre = Docto + "-" + Ide + "-" + i + "-"
                                + DateTime.Now.ToString("yyyy")
                                + DateTime.Now.ToString("MM") 
                                + DateTime.Now.ToString("dd");
                            var fileName = Path.GetFileName(listaArchivos[i].FileName);
                            string ACompleto = Path.Combine(RutaCompleta, Nombre + Path.GetExtension(listaArchivos[i].FileName));
                            listaArchivos[i].SaveAs(ACompleto);
                            lstDoc.Add(Nombre + Path.GetExtension(listaArchivos[i].FileName));
                        }
                        Mensaje = "Archivos almacenados";
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
                            for (int i = 0; i < listaArchivos.Count; i++)
                            {
                                string Nombre = Docto + "-" + Ide + "-" + i + "-"
                                + DateTime.Now.ToString("yyyy")
                                + DateTime.Now.ToString("MM")
                                + DateTime.Now.ToString("dd");
                                var fileName = Path.GetFileName(listaArchivos[i].FileName);
                                string ACompleto = Path.Combine(RutaCompleta, Nombre + Path.GetExtension(listaArchivos[i].FileName));
                                listaArchivos[i].SaveAs(ACompleto);
                                lstDoc.Add(Nombre + Path.GetExtension(listaArchivos[i].FileName));
                            }
                            Mensaje = "Archivos almacenados";
                        }
                        else
                        {
                            actionResult = Json(new { mensaje = "No se pudo procesar el(los) archivo(s).", uploaded = true });
                        }
                    }
                    actionResult = Json(new { mensaje = Mensaje });
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

            Ide = 0;
            Docto = string.Empty;
            Ubica = string.Empty;
            listaArchivos.Clear();

            return actionResult;
        }

        [HttpPost]
        public ActionResult GuardarD(Int16 Id, Int16 IdDocumento, string Documento,
            byte IdRuta, string Ruta)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioColaborador repositorioColaborador = new RepositorioColaborador();
            DColaboradorAnonymous dColaboradorAnonymous;
            List<DColaboradorAnonymous> lstdColaborador;

            string Mensaje = string.Empty;

            string Ubicacion = DateTime.Now.ToString("yyyy")
                + "\\" + DateTime.Now.ToString("MM")
                + "\\" + DateTime.Now.ToString("dd")
                + "\\" + Id;

            try
            {
                List<string> lstArchivos = new List<string>();
                foreach (var doc in lstDoc)
                {
                    lstArchivos.Add(doc);
                }
                lstDoc.Clear();

                lstdColaborador = new List<DColaboradorAnonymous>();
                foreach (var archivo in lstArchivos)
                {
                    string[] nomCompleto = null;
                    nomCompleto = archivo.Split('.');
                    dColaboradorAnonymous = new DColaboradorAnonymous();
                    dColaboradorAnonymous.IdColaborador = Id;
                    dColaboradorAnonymous.IdDocumento = IdDocumento;
                    dColaboradorAnonymous.Ubicacion = Ubicacion;
                    dColaboradorAnonymous.Nombre = nomCompleto[0];
                    dColaboradorAnonymous.Extension = nomCompleto[1];
                    dColaboradorAnonymous.IdRuta = IdRuta;
                    lstdColaborador.Add(dColaboradorAnonymous);
                }
                DataTable dtDColaborador = ConvertToDataTable(lstdColaborador);

                Mensaje = repositorioColaborador.GuardarDocumentos(dtDColaborador) < 0 ? "Agregado Correctamente" : "Error";
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
    }
}