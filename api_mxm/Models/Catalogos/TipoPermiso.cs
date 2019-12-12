using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_mxm.Models
{
    public class TipoPermiso
    {
        public int TipoPermisoId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Borrado { get; set; }

        public List<Permiso> Permisos { get; set; }
    }
}
