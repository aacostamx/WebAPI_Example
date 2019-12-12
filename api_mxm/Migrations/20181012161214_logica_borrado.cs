using Microsoft.EntityFrameworkCore.Migrations;

namespace api_mxm.Migrations
{
    public partial class logica_borrado : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Estatus",
                table: "Usuarios",
                newName: "Borrado");

            migrationBuilder.RenameColumn(
                name: "Estatus",
                table: "Tokens",
                newName: "Borrado");

            migrationBuilder.RenameColumn(
                name: "Estatus",
                table: "Roles",
                newName: "Borrado");

            migrationBuilder.RenameColumn(
                name: "Estatus",
                table: "Rel_RolesUsuarios",
                newName: "Borrado");

            migrationBuilder.RenameColumn(
                name: "Estatus",
                table: "Registros",
                newName: "Borrado");

            migrationBuilder.RenameColumn(
                name: "Estatus",
                table: "Permisos",
                newName: "Borrado");

            migrationBuilder.RenameColumn(
                name: "Estatus",
                table: "Jerarquias",
                newName: "Borrado");

            migrationBuilder.RenameColumn(
                name: "Estatus",
                table: "Documentos",
                newName: "Borrado");

            migrationBuilder.RenameColumn(
                name: "Estatus",
                table: "Cat_TipoPermisos",
                newName: "Borrado");

            migrationBuilder.RenameColumn(
                name: "Estatus",
                table: "Cat_TipoDocumentos",
                newName: "Borrado");

            migrationBuilder.AddColumn<bool>(
                name: "Borrado",
                table: "Pantallas",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Borrado",
                table: "Pantallas");

            migrationBuilder.RenameColumn(
                name: "Borrado",
                table: "Usuarios",
                newName: "Estatus");

            migrationBuilder.RenameColumn(
                name: "Borrado",
                table: "Tokens",
                newName: "Estatus");

            migrationBuilder.RenameColumn(
                name: "Borrado",
                table: "Roles",
                newName: "Estatus");

            migrationBuilder.RenameColumn(
                name: "Borrado",
                table: "Rel_RolesUsuarios",
                newName: "Estatus");

            migrationBuilder.RenameColumn(
                name: "Borrado",
                table: "Registros",
                newName: "Estatus");

            migrationBuilder.RenameColumn(
                name: "Borrado",
                table: "Permisos",
                newName: "Estatus");

            migrationBuilder.RenameColumn(
                name: "Borrado",
                table: "Jerarquias",
                newName: "Estatus");

            migrationBuilder.RenameColumn(
                name: "Borrado",
                table: "Documentos",
                newName: "Estatus");

            migrationBuilder.RenameColumn(
                name: "Borrado",
                table: "Cat_TipoPermisos",
                newName: "Estatus");

            migrationBuilder.RenameColumn(
                name: "Borrado",
                table: "Cat_TipoDocumentos",
                newName: "Estatus");
        }
    }
}
