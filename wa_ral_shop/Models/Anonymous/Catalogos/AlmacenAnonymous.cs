using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wa_ral_shop.Models.Anonymous.Catalogos
{
    public class AlmacenAnonymous
    {
        public byte Id { get; set; }
        public string Almacen { get; set; }

        public DateTime FechaHoraCaptura { get; set; }
        public bool Estatus { get; set; }
        public string EstatusSTR { get; set; }
    }
}