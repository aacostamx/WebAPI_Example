using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api_mxm.Models
{
    public class Documento
    {
        public long DocumentoId { get; set; }
        [ForeignKey("Usuario")]
        public long UsuarioId { get; set; }
        [ForeignKey("TipoDcoumento")]
        public int TipoDocumentoId { get; set; }
        public int CursoId { get; set; }
        public string Contenido { get; set; }
        public bool Borrado { get; set; }

        public virtual TipoDocumento TipoDocumento { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
