using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace api_mxm.Migrations
{
    public partial class AddRoles_y_permisos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cat_TipoPermisos",
                columns: table => new
                {
                    TipoPermisoId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cat_TipoPermisos", x => x.TipoPermisoId);
                });

            migrationBuilder.CreateTable(
                name: "Pantallas",
                columns: table => new
                {
                    PantallaId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pantallas", x => x.PantallaId);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RolId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Descripcion = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RolId);
                });

            migrationBuilder.CreateTable(
                name: "Tokens",
                columns: table => new
                {
                    TokenId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<long>(nullable: false),
                    Fecha = table.Column<DateTime>(nullable: false),
                    Cadena = table.Column<string>(nullable: true),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tokens", x => x.TokenId);
                    table.ForeignKey(
                        name: "FK_Tokens_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rel_RolesUsuarios",
                columns: table => new
                {
                    RolUsuarioId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UsuarioId = table.Column<long>(nullable: false),
                    RolId = table.Column<int>(nullable: false),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rel_RolesUsuarios", x => x.RolUsuarioId);
                    table.ForeignKey(
                        name: "FK_Rel_RolesUsuarios_Roles_RolId",
                        column: x => x.RolId,
                        principalTable: "Roles",
                        principalColumn: "RolId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rel_RolesUsuarios_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "UsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jerarquias",
                columns: table => new
                {
                    JerarquiaId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RolUsuarioId = table.Column<long>(nullable: false),
                    Global = table.Column<int>(nullable: false),
                    EmpresaId = table.Column<int>(nullable: false),
                    ProyectoId = table.Column<int>(nullable: false),
                    CursoId = table.Column<int>(nullable: false),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jerarquias", x => x.JerarquiaId);
                    table.ForeignKey(
                        name: "FK_Jerarquias_Rel_RolesUsuarios_RolUsuarioId",
                        column: x => x.RolUsuarioId,
                        principalTable: "Rel_RolesUsuarios",
                        principalColumn: "RolUsuarioId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Permisos",
                columns: table => new
                {
                    PermisoId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    JerarquiaId = table.Column<long>(nullable: false),
                    PantallaId = table.Column<int>(nullable: false),
                    TipoPermisoId = table.Column<int>(nullable: false),
                    Estatus = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permisos", x => x.PermisoId);
                    table.ForeignKey(
                        name: "FK_Permisos_Jerarquias_JerarquiaId",
                        column: x => x.JerarquiaId,
                        principalTable: "Jerarquias",
                        principalColumn: "JerarquiaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permisos_Pantallas_PantallaId",
                        column: x => x.PantallaId,
                        principalTable: "Pantallas",
                        principalColumn: "PantallaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Permisos_Cat_TipoPermisos_TipoPermisoId",
                        column: x => x.TipoPermisoId,
                        principalTable: "Cat_TipoPermisos",
                        principalColumn: "TipoPermisoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Jerarquias_RolUsuarioId",
                table: "Jerarquias",
                column: "RolUsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_JerarquiaId",
                table: "Permisos",
                column: "JerarquiaId");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_PantallaId",
                table: "Permisos",
                column: "PantallaId");

            migrationBuilder.CreateIndex(
                name: "IX_Permisos_TipoPermisoId",
                table: "Permisos",
                column: "TipoPermisoId");

            migrationBuilder.CreateIndex(
                name: "IX_Rel_RolesUsuarios_RolId",
                table: "Rel_RolesUsuarios",
                column: "RolId");

            migrationBuilder.CreateIndex(
                name: "IX_Rel_RolesUsuarios_UsuarioId",
                table: "Rel_RolesUsuarios",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Tokens_UsuarioId",
                table: "Tokens",
                column: "UsuarioId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permisos");

            migrationBuilder.DropTable(
                name: "Tokens");

            migrationBuilder.DropTable(
                name: "Jerarquias");

            migrationBuilder.DropTable(
                name: "Pantallas");

            migrationBuilder.DropTable(
                name: "Cat_TipoPermisos");

            migrationBuilder.DropTable(
                name: "Rel_RolesUsuarios");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
