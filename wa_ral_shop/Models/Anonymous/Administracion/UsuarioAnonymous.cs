using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wa_ral_shop.Models.Anonymous.Administracion
{
    public class UsuarioAnonymous
    {
        public int Id { get; set; }
        public string Usuario { get; set; }
        public string PSW { get; set; }
        public byte TUsuario { get; set; }
        public DateTime FechaHoraCaptura { get; set; }
        public bool Estatus { get; set; }
        public string EstatusSTR { get; set; }
    }
}