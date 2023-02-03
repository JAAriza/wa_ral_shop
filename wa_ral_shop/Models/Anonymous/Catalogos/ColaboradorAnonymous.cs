using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wa_ral_shop.Models.Anonymous.Catalogos
{
    public class ColaboradorAnonymous
    {
        public Int16 Id { get; set; }
        public Int16 IdPuesto { get; set; }
        public PuestoAnonymous PuestoAnonymous { get; set; }
        public string Nombre { get; set; }
        public string APaterno { get; set; }
        public string AMaterno { get; set; }
        public string Telefono { get; set; }
        public string EMail { get; set; }
        public DateTime FechaHoraContratacion { get; set; }
        public DateTime FechaHoraCaptura { get; set; }
        public bool Estatus { get; set; }
        public string EstatusSTR { get; set; }
    }
}