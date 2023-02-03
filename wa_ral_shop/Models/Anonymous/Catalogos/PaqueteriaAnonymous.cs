﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wa_ral_shop.Models.Anonymous.Catalogos
{
    public class PaqueteriaAnonymous
    {
        public Int16 Id { get; set; }
        public string Nombre { get; set; }
        public string Estrellas { get; set; }
        public DateTime FechaHoraCaptura { get; set; }
        public bool Estatus { get; set; }
        public string EstatusSTR { get; set; }
        public string Comentario { get; set; }
    }
}