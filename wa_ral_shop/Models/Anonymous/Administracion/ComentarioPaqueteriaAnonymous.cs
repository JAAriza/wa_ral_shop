using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wa_ral_shop.Models.Anonymous.Catalogos;

namespace wa_ral_shop.Models.Anonymous.Administracion
{
    public class ComentarioPaqueteriaAnonymous
    {
        public Int16 Id { get; set; }
        public Int16 IdPaqueteria { get; set; }
        public PaqueteriaAnonymous PaqueteriaAnonymous { get; set; }
        public string Comentario { get; set; }
        public DateTime FechaHoraCaptura { get; set; }
        public bool Estatus { get; set; }
        public string EstatusSTR { get; set; }
    }
}