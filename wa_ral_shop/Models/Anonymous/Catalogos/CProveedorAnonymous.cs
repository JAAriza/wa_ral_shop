using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wa_ral_shop.Models.Anonymous.Catalogos
{
    public class CProveedorAnonymous
    {
        public int Id { get; set; }
        public int IdProveedor { get; set; }
        public ProveedorAnonymous proveedorAnonymous { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaHoraCaptura { get; set; }
        public bool Estatus { get; set; }
        public string EstatusSTR { get; set; }
    }
}