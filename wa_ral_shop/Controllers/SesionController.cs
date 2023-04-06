using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using wa_ral_shop.Models.Utilerias;
using wa_ral_shop.Models.Repositorios;
using wa_ral_shop.Models.Anonymous.Catalogos;
using wa_ral_shop.Models.Repositorios.Administracion;
using System.Data;
using wa_ral_shop.Models.Anonymous;
using wa_ral_shop.Models.Anonymous.Administracion;

namespace wa_ral_shop.Controllers
{
    [Serializable()]
    [AllowAnonymous]
    public class SesionController : Controller
    {
        // GET: Sesion
        public ActionResult Login()
        {
            Info info = new Info();
            Session["TUsuario"] = info.GetTUsuario();//Se agrega para evitar error en validacion en layout
            return View();
        }
        [HttpPost]
        public ActionResult Iniciar(string email, string password)
        {
            RepositorioSesion repositorioSesion = new RepositorioSesion();
            string UserEnc;
            string PassEnc;
            int Inicio;
            Info info = new Info();
            try
            {
                UserEnc = EncYDec.EncryptPlainTextToCipherText(email);
                PassEnc = EncYDec.EncryptPlainTextToCipherText(password);
                Inicio = repositorioSesion.ValidaLogin(UserEnc, PassEnc, info.GetTUsuario());
                if (Inicio <= 0)
                {
                    //Mensaje de error                    
                    return View("ErrorLogin");
                }
                else
                {
                    //Consultar Objeto cliente
                    ClienteAnonymous clienteAnonymous = new ClienteAnonymous();
                    DataTable dtCliente = new DataTable();
                    dtCliente = repositorioSesion.BuscarCliente(Inicio);
                    clienteAnonymous.Id = int.Parse(dtCliente.Rows[0][0].ToString());
                    clienteAnonymous.Nombre = dtCliente.Rows[0][1].ToString();
                    clienteAnonymous.APaterno = dtCliente.Rows[0][2].ToString();
                    clienteAnonymous.AMaterno = dtCliente.Rows[0][3].ToString();
                    clienteAnonymous.Telefono = dtCliente.Rows[0][5].ToString();
                    Session["Nombre"] = clienteAnonymous.Nombre;
                    Session["APaterno"] = clienteAnonymous.APaterno;
                    Session["AMaterno"] = clienteAnonymous.AMaterno;
                    Session["Telefono"] = clienteAnonymous.Telefono;
                    Session["EMail"] = email;
                    Session["Ide"] = Inicio;//Ide es el IdCliente
                    Session["TUsuario"] = info.GetTUsuario();//1= Cliente, 2 = Admin del sistema

                    //Agregar consulta de permisos por pantalla por usuario
                    if (int.Parse(Session["TUsuario"].ToString()) == 1)//Cliente
                    {
                        //Administracion
                        Session["CDireccion"] = 0;//ClienteDireccion
                        Session["Cliente"] = 0;//Cliente
                        Session["Compra"] = 0;//Compra
                        Session["CPaqueteria"] = 0;//ComentarioPaqueteria
                        Session["CProveedor"] = 0;//ComentarioProveedor
                        Session["Proveedor"] = 0;//Proveedor
                        
                        //Catalogos
                        Session["Carrito"] = 1;//CarritoCompras
                        Session["Categoria"] = 0;//Categoria
                        Session["Colaborador"] = 0;//Colaborador
                        Session["Cuenta"] = 1;//Cuenta
                        Session["Documento"] = 0;//Documento
                        Session["Favorito"] = 1;//Favorito
                        Session["Paqueteria"] = 0;//Paqueteria
                        Session["Porcentaje"] = 0;//Porcentaje
                        Session["Producto"] = 0;//Producto
                        Session["Puesto"] = 0;//Puesto
                        Session["RutaBase"] = 0;//RutaBase
                        Session["UnidadMedida"] = 0;//UnidadMedida
                    }
                    if (int.Parse(Session["TUsuario"].ToString()) == 2)//Admin/Colab
                    {
                        //Administracion
                        Session["CDireccion"] = 1;//ClienteDireccion
                        Session["Cliente"] = 1;//Cliente
                        Session["Compra"] = 1;//Compra
                        Session["CPaqueteria"] = 1;//ComentarioPaqueteria
                        Session["CProveedor"] = 1;//ComentarioProveedor
                        Session["Proveedor"] = 1;//Proveedor

                        //Catalogos
                        Session["Carrito"] = 0;//CarritoCompras
                        Session["Categoria"] = 1;//Categoria
                        Session["Colaborador"] = 1;//Colaborador
                        Session["Cuenta"] = 0;//Cuenta
                        Session["Documento"] = 1;//Documento
                        Session["Favorito"] = 0;//Favorito
                        Session["Paqueteria"] = 1;//Paqueteria
                        Session["Porcentaje"] = 1;//Porcentaje
                        Session["Producto"] = 1;//Producto
                        Session["Puesto"] = 1;//Puesto
                        Session["RutaBase"] = 1;//RutaBase
                        Session["UnidadMedida"] = 1;//UnidadMedida
                    }
                    

                    return RedirectToAction("Principal", "Inicio");
                }
            }
            catch (Exception)
            {
                return View("ErrorLogin");
                //throw;
            }
        }

        public void LogOut()
        {
            Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            FormsAuthentication.SignOut();
            
            Session.Abandon();
            Session.Clear();
            
            HttpCookie cookie1 = new HttpCookie(FormsAuthentication.FormsCookieName, "");
            cookie1.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(cookie1);
            FormsAuthentication.RedirectToLoginPage();
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult Alta(string Nombre, string APaterno, string AMaterno, 
            string Telefono, string EMail, string Pass)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioSesion repositorioSesion = new RepositorioSesion();
            ClienteAnonymous clienteAnonymous = new ClienteAnonymous();
            int Id = 0;
            string Mensaje = string.Empty;
            string EMailEnc;
            string PassEnc;

            clienteAnonymous.Nombre = Nombre;
            clienteAnonymous.APaterno = APaterno;
            clienteAnonymous.AMaterno = AMaterno;
            //clienteAnonymous.Estrellas = Estrellas;
            clienteAnonymous.Telefono = Telefono;
            clienteAnonymous.EMail = EMail;
            EMailEnc = EncYDec.EncryptPlainTextToCipherText(EMail);
            PassEnc = EncYDec.EncryptPlainTextToCipherText(Pass);

            try
            {
                Id = repositorioSesion.Alta(clienteAnonymous);
                if (Id > 0)
                {
                    if (repositorioSesion.AltaU(Id, PassEnc, EMailEnc) > 0)
                    {
                        Mensaje = "Agregado Correctamente";
                    }
                    else
                    {
                        repositorioSesion.EliminarFalloU(Id);
                        Mensaje = "Error";
                    }                    
                }
                else
                {
                    Mensaje = "Error";
                }
                
                actionResult = Json(new { mensaje = Mensaje, Ide = Id });
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
        public ActionResult BuscarEdoyMpio(int CP)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioSesion repositorioSesion = new RepositorioSesion();
            DataTable dtEdoMpio = new DataTable();
            string Estado = string.Empty;
            string Municipio = string.Empty;
            string Mensaje = string.Empty;

            try
            {
                dtEdoMpio = repositorioSesion.BuscarEdoyMpioPorCP(CP);
                if (dtEdoMpio.Rows.Count > 0)
                {
                    Estado = dtEdoMpio.Rows[0][0].ToString();
                    Municipio = dtEdoMpio.Rows[0][1].ToString();
                    Mensaje = "Estado y Municipio Encontrados";
                }
                else
                {
                    ContentResultObject.Codigo = "SinDatos";
                    ContentResultObject.Mensaje = "Sin datos encontrados";
                }
                actionResult = Json(new { ContentResultObject.Codigo, mensaje = Mensaje, Edo = Estado, Mpio = Municipio });
            }
            catch (Exception Ex)
            {
                ContentResultObject.Codigo = "Error";
                ContentResultObject.Mensaje = Ex.Message;
                actionResult = Json(new { ContentResultObject.Codigo, mensaje = ContentResultObject.Mensaje });
            }
            return actionResult;
        }

        [HttpPost]
        //[ValidateInput(false)]
        public ActionResult BuscarColonias(int CP)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioSesion repositorioSesion = new RepositorioSesion();
            DataTable dtColonias = new DataTable();
            List<ComboAnonymous> lstColonias = new List<ComboAnonymous>();
            ComboAnonymous cmbColonias;
            string Mensaje = string.Empty;

            try
            {
                dtColonias = repositorioSesion.BuscarColoniasPorCP(CP);
                if (dtColonias.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtColonias.Rows)
                    {
                        cmbColonias = new ComboAnonymous();
                        cmbColonias.Dato = dr[0].ToString();
                        lstColonias.Add(cmbColonias);
                    }
                    Mensaje = "Colonias Encontradas";
                }
                else
                {
                    ContentResultObject.Codigo = "SinDatos";
                    ContentResultObject.Mensaje = "Sin datos encontrados";
                }
                actionResult = Json(new { mensaje = Mensaje, comboColonias = lstColonias });
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
        public ActionResult GDireccion(int IdCliente, int CP, string Colonia, string Calle
            , string NExterior, string NInterior, string Nombre, string Telefono
                , string Descripcion, string EntreCalle, string YCalle)
        {
            ContentResultObject ContentResultObject = new ContentResultObject();
            ActionResult actionResult = null;
            RepositorioSesion repositorioSesion = new RepositorioSesion();
            CDireccionAnonymous cDireccionAnonymous = new CDireccionAnonymous();
            string Mensaje = string.Empty;

            cDireccionAnonymous.IdCliente = IdCliente;
            cDireccionAnonymous.CP = CP;
            cDireccionAnonymous.Colonia = Colonia;
            cDireccionAnonymous.Calle = Calle;
            cDireccionAnonymous.NExterior = NExterior;
            cDireccionAnonymous.NInterior = NInterior;
            cDireccionAnonymous.Nombre = Nombre;
            cDireccionAnonymous.Telefono = Telefono;
            cDireccionAnonymous.Descripcion = Descripcion;
            cDireccionAnonymous.EntreCalle = EntreCalle;
            cDireccionAnonymous.YCalle = YCalle;

            try
            {
                Mensaje = repositorioSesion.AltaD(cDireccionAnonymous) > 0 ? "Agregado Correctamente" : "Error";
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