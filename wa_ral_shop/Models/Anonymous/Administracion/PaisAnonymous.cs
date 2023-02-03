using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wa_ral_shop.Models.Anonymous.Administracion
{
    public class PaisAnonymous
    {
        public int Id { get; set; }
        public int ISONUM { get; set; }
        public string ISO2 { get; set; }
        public string ISO3 { get; set; }
        public string Nombre { get; set; }
    }
}