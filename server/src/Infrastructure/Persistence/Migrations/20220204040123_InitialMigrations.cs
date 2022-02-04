using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class InitialMigrations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "common");

            migrationBuilder.CreateTable(
                name: "cs_entity_types",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    c_name = table.Column<string>(type: "text", nullable: false),
                    c_slug = table.Column<string>(type: "text", nullable: false),
                    c_schema = table.Column<string>(type: "text", nullable: false),
                    c_tablename = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cs_entity_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cd_entities",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    f_type = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cd_entities", x => new { x.id, x.f_type });
                    table.ForeignKey(
                        name: "FK_cd_entities_cs_entity_types_f_type",
                        column: x => x.f_type,
                        principalSchema: "common",
                        principalTable: "cs_entity_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cd_entities_f_type",
                schema: "common",
                table: "cd_entities",
                column: "f_type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cd_entities",
                schema: "common");

            migrationBuilder.DropTable(
                name: "cs_entity_types",
                schema: "common");
        }
    }
}
