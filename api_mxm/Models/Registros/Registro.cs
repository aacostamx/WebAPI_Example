using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace api_mxm.Models
{
    public class Registro
    {
        public long RegistroId { get; set; }
        [ForeignKey("Usuario")]
        public long UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Paterno  { get; set; }
        public string Materno { get; set; }
        public string CURP { get; set; }
        public string Movil { get; set; }
        public string Fijo { get; set; }
        public string CodigoPostal { get; set; }
        public string Domicilio { get; set; }
        public string Exterior { get; set; }
        public string Interior { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public bool Borrado { get; set; }

        public virtual Usuario Usuario { get; set; }

    }
}
