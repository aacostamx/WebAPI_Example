﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api_mxm.Models;

namespace api_mxm.Migrations
{
    [DbContext(typeof(MXMContext))]
    [Migration("20181009151351_AddRoles_y_permisos")]
    partial class AddRoles_y_permisos
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.3-rtm-32065")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("api_mxm.Models.Documento", b =>
                {
                    b.Property<long>("DocumentoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Contenido");

                    b.Property<int>("CursoId");

                    b.Property<bool>("Estatus");

                    b.Property<int>("TipoDocumentoId");

                    b.Property<long>("UsuarioId");

                    b.HasKey("DocumentoId");

                    b.HasIndex("TipoDocumentoId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Documentos");
                });

            modelBuilder.Entity("api_mxm.Models.Jerarquia", b =>
                {
                    b.Property<long>("JerarquiaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("CursoId");

                    b.Property<int>("EmpresaId");

                    b.Property<bool>("Estatus");

                    b.Property<int>("Global");

                    b.Property<int>("ProyectoId");

                    b.Property<long>("RolUsuarioId");

                    b.HasKey("JerarquiaId");

                    b.HasIndex("RolUsuarioId");

                    b.ToTable("Jerarquias");
                });

            modelBuilder.Entity("api_mxm.Models.Pantalla", b =>
                {
                    b.Property<int>("PantallaId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion");

                    b.Property<string>("Nombre");

                    b.HasKey("PantallaId");

                    b.ToTable("Pantallas");
                });

            modelBuilder.Entity("api_mxm.Models.Permiso", b =>
                {
                    b.Property<long>("PermisoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Estatus");

                    b.Property<long>("JerarquiaId");

                    b.Property<int>("PantallaId");

                    b.Property<int>("TipoPermisoId");

                    b.HasKey("PermisoId");

                    b.HasIndex("JerarquiaId");

                    b.HasIndex("PantallaId");

                    b.HasIndex("TipoPermisoId");

                    b.ToTable("Permisos");
                });

            modelBuilder.Entity("api_mxm.Models.Registro", b =>
                {
                    b.Property<long>("RegistroId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("CURP");

                    b.Property<string>("CodigoPostal");

                    b.Property<string>("Domicilio");

                    b.Property<bool>("Estatus");

                    b.Property<string>("Exterior");

                    b.Property<DateTime>("FechaNacimiento");

                    b.Property<string>("Fijo");

                    b.Property<string>("Interior");

                    b.Property<string>("Materno");

                    b.Property<string>("Movil");

                    b.Property<string>("Nombre");

                    b.Property<string>("Paterno");

                    b.Property<long>("UsuarioId");

                    b.HasKey("RegistroId");

                    b.HasIndex("UsuarioId")
                        .IsUnique();

                    b.ToTable("Registros");
                });

            modelBuilder.Entity("api_mxm.Models.Rol", b =>
                {
                    b.Property<int>("RolId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion");

                    b.Property<bool>("Estatus");

                    b.Property<string>("Nombre");

                    b.HasKey("RolId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("api_mxm.Models.RolUsuario", b =>
                {
                    b.Property<long>("RolUsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Estatus");

                    b.Property<int>("RolId");

                    b.Property<long>("UsuarioId");

                    b.HasKey("RolUsuarioId");

                    b.HasIndex("RolId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Rel_RolesUsuarios");
                });

            modelBuilder.Entity("api_mxm.Models.TipoDocumento", b =>
                {
                    b.Property<int>("TipoDocumentoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion");

                    b.Property<bool>("Estatus");

                    b.Property<string>("Nombre");

                    b.HasKey("TipoDocumentoId");

                    b.ToTable("Cat_TipoDocumentos");
                });

            modelBuilder.Entity("api_mxm.Models.TipoPermiso", b =>
                {
                    b.Property<int>("TipoPermisoId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descripcion");

                    b.Property<bool>("Estatus");

                    b.Property<string>("Nombre");

                    b.HasKey("TipoPermisoId");

                    b.ToTable("Cat_TipoPermisos");
                });

            modelBuilder.Entity("api_mxm.Models.Token", b =>
                {
                    b.Property<long>("TokenId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Cadena");

                    b.Property<bool>("Estatus");

                    b.Property<DateTime>("Fecha");

                    b.Property<long>("UsuarioId");

                    b.HasKey("TokenId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Tokens");
                });

            modelBuilder.Entity("api_mxm.Models.Usuario", b =>
                {
                    b.Property<long>("UsuarioId")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("Actualizado");

                    b.Property<string>("Avatar");

                    b.Property<DateTime>("Creado");

                    b.Property<bool>("Estatus");

                    b.Property<string>("Mail");

                    b.Property<string>("Password");

                    b.Property<string>("Username");

                    b.HasKey("UsuarioId");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("api_mxm.Models.Documento", b =>
                {
                    b.HasOne("api_mxm.Models.TipoDocumento", "TipoDocumento")
                        .WithMany("Documentos")
                        .HasForeignKey("TipoDocumentoId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("api_mxm.Models.Usuario", "Usuario")
                        .WithMany("Documentos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("api_mxm.Models.Jerarquia", b =>
                {
                    b.HasOne("api_mxm.Models.RolUsuario", "RolUsuario")
                        .WithMany()
                        .HasForeignKey("RolUsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("api_mxm.Models.Permiso", b =>
                {
                    b.HasOne("api_mxm.Models.Jerarquia", "Jerarquia")
                        .WithMany("Permisos")
                        .HasForeignKey("JerarquiaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("api_mxm.Models.Pantalla", "Pantalla")
                        .WithMany("Permisos")
                        .HasForeignKey("PantallaId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("api_mxm.Models.TipoPermiso", "TipoPermiso")
                        .WithMany("Permisos")
                        .HasForeignKey("TipoPermisoId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("api_mxm.Models.Registro", b =>
                {
                    b.HasOne("api_mxm.Models.Usuario", "Usuario")
                        .WithOne("Registro")
                        .HasForeignKey("api_mxm.Models.Registro", "UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("api_mxm.Models.RolUsuario", b =>
                {
                    b.HasOne("api_mxm.Models.Rol", "Rol")
                        .WithMany("RolesUsuarios")
                        .HasForeignKey("RolId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("api_mxm.Models.Usuario", "Usuario")
                        .WithMany("RolesUsuarios")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("api_mxm.Models.Token", b =>
                {
                    b.HasOne("api_mxm.Models.Usuario", "Usuario")
                        .WithMany("Tokens")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
