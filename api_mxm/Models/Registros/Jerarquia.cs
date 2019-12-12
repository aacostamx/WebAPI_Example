using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api_mxm.Models
{
    public class Jerarquia
    {
        public Jerarquia()
        {
            this.Permisos = new HashSet<Permiso>();
        }

        public long JerarquiaId { get; set; }
        [ForeignKey("RolUsuario")]
        public long RolUsuarioId { get; set; }
        public int Global { get; set; }
        public int EmpresaId { get; set; }
        public int ProyectoId { get; set; }
        public int CursoId { get; set; }
        public bool Borrado { get; set; }

        public virtual RolUsuario RolUsuario { get; set; }
        public virtual ICollection<Permiso> Permisos { get; set; }
    }
}
