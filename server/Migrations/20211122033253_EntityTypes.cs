using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class EntityTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TableName",
                schema: "admin",
                table: "cs_entity_types",
                newName: "c_tablename");

            migrationBuilder.RenameColumn(
                name: "Slug",
                schema: "admin",
                table: "cs_entity_types",
                newName: "c_slug");

            migrationBuilder.RenameColumn(
                name: "Schema",
                schema: "admin",
                table: "cs_entity_types",
                newName: "c_schema");

            migrationBuilder.RenameColumn(
                name: "Name",
                schema: "admin",
                table: "cs_entity_types",
                newName: "c_name");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "c_tablename",
                schema: "admin",
                table: "cs_entity_types",
                newName: "TableName");

            migrationBuilder.RenameColumn(
                name: "c_slug",
                schema: "admin",
                table: "cs_entity_types",
                newName: "Slug");

            migrationBuilder.RenameColumn(
                name: "c_schema",
                schema: "admin",
                table: "cs_entity_types",
                newName: "Schema");

            migrationBuilder.RenameColumn(
                name: "c_name",
                schema: "admin",
                table: "cs_entity_types",
                newName: "Name");
        }
    }
}
