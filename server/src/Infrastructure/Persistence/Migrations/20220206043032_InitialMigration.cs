using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class InitialMigration : Migration
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
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "идентификатор"),
                    c_name = table.Column<string>(type: "text", nullable: false, comment: "название"),
                    c_slug = table.Column<string>(type: "text", nullable: false, comment: "код"),
                    c_schema = table.Column<string>(type: "text", nullable: false, comment: "схема"),
                    c_tablename = table.Column<string>(type: "text", nullable: false, comment: "таблица")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cs_entity_types", x => x.id);
                },
                comment: "типы сущностей");

            migrationBuilder.CreateTable(
                name: "cd_entities",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false, comment: "идентификатор"),
                    f_type = table.Column<Guid>(type: "uuid", nullable: false, comment: "тип")
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_entities", x => new { x.id, x.f_type });
                    table.ForeignKey(
                        name: "fk_cd_entities_cs_entity_types_f_type",
                        column: x => x.f_type,
                        principalSchema: "common",
                        principalTable: "cs_entity_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "сущности");

            migrationBuilder.CreateIndex(
                name: "ix_cd_entities_f_type",
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
