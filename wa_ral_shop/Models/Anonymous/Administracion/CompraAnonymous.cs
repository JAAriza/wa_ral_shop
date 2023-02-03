using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wa_ral_shop.Models.Anonymous.Administracion
{
    public class CompraAnonymous
    {
        public int Id { get; set; }
        public decimal CostoEnvio { get; set; }
        public DateTime FechaHoraAvisoImportacion { get; set; }
        public DateTime FechaHoraLlegadaMX { get; set; }
        public decimal CostoTotal { get; set; }
        public DateTime FechaHoraCaptura { get; set; }
        public string Estatus { get; set; }   
    }
}