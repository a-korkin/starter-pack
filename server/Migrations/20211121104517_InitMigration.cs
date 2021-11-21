using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class InitMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "common");

            migrationBuilder.CreateTable(
                name: "cd_entities",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_entities", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cd_persons",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    c_last_name = table.Column<string>(nullable: false),
                    c_first_name = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_persons", x => x.id);
                    table.ForeignKey(
                        name: "fk_cd_persons_cd_entities_id",
                        column: x => x.id,
                        principalSchema: "common",
                        principalTable: "cd_entities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cd_persons",
                schema: "common");

            migrationBuilder.DropTable(
                name: "cd_entities",
                schema: "common");
        }
    }
}
