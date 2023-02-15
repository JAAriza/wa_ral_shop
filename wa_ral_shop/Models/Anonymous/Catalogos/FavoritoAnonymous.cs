using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wa_ral_shop.Models.Anonymous.Catalogos
{
    public class FavoritoAnonymous
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public ProductoAnonymous ProductoA { get; set; }
        public int IdCliente { get; set; }
        public DateTime FechaHoraCaptura { get; set; }
    }
}