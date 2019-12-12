using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_mxm.Models
{
    public class MXMContext : DbContext
    {
        public MXMContext(DbContextOptions<MXMContext> options) : base(options)
        { }

        
        #region REGISTROS
        public DbSet<Documento> Documentos { get; set; }
        public DbSet<Jerarquia> Jerarquias { get; set; }
        public DbSet<Permiso> Permisos { get; set; }
        public DbSet<Registro> Registros { get; set; }
        public DbSet<Token> Tokens { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        #endregion

        #region Catalogos
        public DbSet<Pantalla> Pantallas { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<TipoDocumento> Cat_TipoDocumentos { get; set; }
        public DbSet<TipoPermiso> Cat_TipoPermisos { get; set; }
        #endregion

        #region RELACIONES
        public DbSet<RolUsuario> Rel_RolesUsuarios { get; set; }

        #endregion

    }
}
