using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace wa_ral_shop.Models.Anonymous.Catalogos
{
    public class ProductoAnonymous
    {
        public int Id { get; set; }
        public byte IdCategoria { get; set; }
        public CategoriaAnonymous categoriaAnonymous { get; set; }
        public string Nombre { get; set; }
        public string Modelo { get; set; }
        public string Descripcion { get; set; }
        public float Largo { get; set; }
        public float Ancho { get; set; }
        public float Alto { get; set; }
        public byte IdUMedidaT { get; set; }
        public UnidadMedidaAnonymous unidadMedidaT { get; set; }
        public float Peso { get; set; }
        public byte IdUMedidaP { get; set; }
        public UnidadMedidaAnonymous unidadMedidaP { get; set; }
        public string CodBarras { get; set; }
        public decimal PrecioVenta { get; set; }
        public int Existencias { get; set; }
        public DateTime FechaHoraCaptura { get; set; }
        public bool Estatus { get; set; }
        public string EstatusSTR { get; set; }
    }
}