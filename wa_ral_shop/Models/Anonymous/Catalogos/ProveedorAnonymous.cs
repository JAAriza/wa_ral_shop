using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wa_ral_shop.Models.Anonymous.Administracion;

namespace wa_ral_shop.Models.Anonymous.Catalogos
{
    public class ProveedorAnonymous
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int IdPais { get; set; }
        public PaisAnonymous Paises { get; set; }
        public string Telefono { get; set; }
        public string Telefono2 { get; set; }
        public string EMail { get; set; }
        public string EMail2 { get; set; }
        public byte Estrellas { get; set; }
        //public string Comentario { get; set; }
        public DateTime FechaHoraCaptura { get; set; }
        public bool Estatus { get; set; }
        public string EstatusSTR { get; set; }
    }
}