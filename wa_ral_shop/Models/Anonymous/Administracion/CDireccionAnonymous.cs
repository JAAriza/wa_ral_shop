using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wa_ral_shop.Models.Anonymous.Catalogos;

namespace wa_ral_shop.Models.Anonymous.Administracion
{
    public class CDireccionAnonymous
    {
        public int Id { get; set; }
        public int IdCliente { get; set; }
        public ClienteAnonymous ClienteAnonymous { get; set; }
        public int CP { get; set; }
        public string Estado { get; set; }
        public string Municipio { get; set; }
        public string Colonia { get; set; }
        public string Calle { get; set; }
        public string NExterior { get; set; }
        public string NInterior { get; set; }
        public string Nombre { get; set; }
        public string Telefono { get; set; }
        public string Descripcion { get; set; }
        public string EntreCalle { get; set; }
        public string YCalle { get; set; }
    }
}