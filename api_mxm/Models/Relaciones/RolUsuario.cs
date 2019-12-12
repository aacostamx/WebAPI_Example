using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api_mxm.Models
{
    public class RolUsuario
    {
        public RolUsuario()
        {
            this.Jerarquias = new HashSet<Jerarquia>();
        }
        public long RolUsuarioId { get; set; }
        [ForeignKey("Usuario")]
        public long UsuarioId { get; set; }
        [ForeignKey("Rol")]
        public int RolId { get; set; }
        public bool Borrado { get; set; }

        public Usuario Usuario { get; set; }
        public Rol Rol { get; set; }
        public virtual ICollection<Jerarquia> Jerarquias { get; set; }
    }
}
