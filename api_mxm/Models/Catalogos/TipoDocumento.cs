
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_mxm.Models
{
    public class TipoDocumento
    {
        public TipoDocumento()
        {
            this.Documentos = new HashSet<Documento>();
        }

        public int TipoDocumentoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Borrado { get; set; }

        public virtual ICollection<Documento> Documentos { get; set; }
    }
}
