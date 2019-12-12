using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api_mxm.Models
{
    public class Token
    {
        public long TokenId { get; set; }
        [ForeignKey("Usuario")]
        public long UsuarioId { get; set; }
        public DateTime Fecha { get; set; }
        public string Cadena { get; set; }
        public bool Borrado { get; set; }

        public virtual Usuario Usuario { get; set; }
    }

    public class Response
    {
        public string response { get; set; }
    }

}
