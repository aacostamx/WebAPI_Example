using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api_mxm.Models
{
    public class Permiso
    {
        public long PermisoId { get; set; }
        [ForeignKey("Jerarquia")]
        public long JerarquiaId { get; set; }
        [ForeignKey("Pantalla")]
        public int PantallaId { get; set; }
        [ForeignKey("TipoPermiso")]
        public int TipoPermisoId { get; set; }
        public bool Borrado { get; set; }

        public virtual Jerarquia Jerarquia { get; set; }
        public virtual Pantalla Pantalla { get; set; }
        public virtual TipoPermiso TipoPermiso { get; set; }

    }
}
