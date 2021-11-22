using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class EntityTypes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "c_type",
                schema: "common",
                table: "cd_entities");

            migrationBuilder.AddColumn<Guid>(
                name: "f_type",
                schema: "common",
                table: "cd_entities",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_cd_entities_f_type",
                schema: "common",
                table: "cd_entities",
                column: "f_type");

            migrationBuilder.AddForeignKey(
                name: "fk_cd_entities_cs_entitie_types_id",
                schema: "common",
                table: "cd_entities",
                column: "f_type",
                principalSchema: "admin",
                principalTable: "cs_entity_types",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_cd_entities_cs_entitie_types_id",
                schema: "common",
                table: "cd_entities");

            migrationBuilder.DropIndex(
                name: "IX_cd_entities_f_type",
                schema: "common",
                table: "cd_entities");

            migrationBuilder.DropColumn(
                name: "f_type",
                schema: "common",
                table: "cd_entities");

            migrationBuilder.AddColumn<string>(
                name: "c_type",
                schema: "common",
                table: "cd_entities",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
