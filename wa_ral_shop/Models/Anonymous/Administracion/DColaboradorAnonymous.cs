using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using wa_ral_shop.Models.Anonymous.Catalogos;

namespace wa_ral_shop.Models.Anonymous.Administracion
{
    public class DColaboradorAnonymous
    {
        //public int Id { get; set; }
        //public DocumentoAnonymous Documento { get; set; }
        public Int16 IdDocumento { get; set; }
        //public RutaBaseAnonymous Ruta { get; set; }
        public byte IdRuta { get; set; }
        //public ColaboradorAnonymous Colaborador { get; set; }
        public Int16 IdColaborador { get; set; }
        public string Ubicacion { get; set; }
        public string Nombre { get; set; }
        public string Extension { get; set; }
        //public DateTime FechaHoraAlmacenamiento { get; set; }
        //public bool Respaldo { get; set; }
    
    }
}