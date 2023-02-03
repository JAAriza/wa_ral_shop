using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wa_ral_shop.Models.Anonymous.Catalogos
{
    public class ProductoImagenAnonymous
    {
        public int Id { get; set; }
        public int IdProducto { get; set; }
        public ProductoAnonymous productoAnonymous { get; set; }
        public byte IdRutaBase { get; set; }
        public RutaBaseAnonymous rutaBaseAnonymous { get; set; }
        public string Ubicacion { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        public byte Consecutivo { get; set; }
        public DateTime FechaHoraCaptura { get; set; }
        public bool Estatus { get; set; }
    }
}