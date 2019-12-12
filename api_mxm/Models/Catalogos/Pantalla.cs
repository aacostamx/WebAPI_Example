using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_mxm.Models
{
    public class Pantalla
    {
        public Pantalla()
        {
            this.Permisos = new HashSet<Permiso>();
        }

        public int PantallaId { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public bool Borrado { get; set; }

        public virtual ICollection<Permiso> Permisos { get; set; }
    }
}
