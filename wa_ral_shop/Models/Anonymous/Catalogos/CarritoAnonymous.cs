using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wa_ral_shop.Models.Anonymous.Catalogos
{
    public class CarritoAnonymous
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public ProductoAnonymous ProductoA { get; set; }
        public int IdCliente { get; set; }
        public int Cantidad { get; set; }
        public DateTime FechaHoraCaptura { get; set; }
    }
}