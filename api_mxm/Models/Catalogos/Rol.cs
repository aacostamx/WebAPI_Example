using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_mxm.Models
{
    public class Rol
    {
        public Rol()
        {
            this.RolesUsuarios = new HashSet<RolUsuario>();
        }

        public int RolId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Borrado { get; set; }

        public virtual ICollection<RolUsuario> RolesUsuarios { get; set; }
    }
}
