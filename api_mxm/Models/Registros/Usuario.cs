using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_mxm.Models
{
    public class Usuario
    {
        public Usuario()
        {
            this.Documentos = new HashSet<Documento>();
            this.RolesUsuarios = new HashSet<RolUsuario>();
            this.Tokens = new HashSet<Token>();

        }

        public long UsuarioId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Codigo { get; set; }
        public string Mail { get; set; }
        public DateTime Creado { get; set; }
        public DateTime Actualizado { get; set; }
        public string Avatar { get; set; }
        public bool Borrado { get; set; }

        public virtual ICollection<Documento> Documentos { get; set; }
        public virtual ICollection<RolUsuario> RolesUsuarios { get; set; }
        public virtual ICollection<Token> Tokens { get; set; }
        public virtual Registro Registro { get; set; }

    }
}
