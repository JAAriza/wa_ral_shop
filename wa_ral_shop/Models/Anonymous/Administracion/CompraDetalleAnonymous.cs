using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wa_ral_shop.Models.Anonymous.Catalogos;

namespace wa_ral_shop.Models.Anonymous.Administracion
{
    public class CompraDetalleAnonymous
    {
        public int Id { get; set; }
        public int IdCompra { get; set; }
        public CompraAnonymous CompraA { get; set; }
        public int IdProducto { get; set; }
        public ProductoAnonymous ProductoA { get; set; }
        public int Cantidad { get; set; }
        public int IdProveedor { get; set; }
        public ProveedorAnonymous ProveedorA { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal CostoEnvio { get; set; }
        public DateTime FechaHoraCompra { get; set; }
        public DateTime FechaHoraLlegadaUSA { get; set; }
    }
}