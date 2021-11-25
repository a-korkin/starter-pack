using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class Klaims : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cd_claims",
                schema: "admin",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    c_type = table.Column<string>(nullable: false),
                    c_value = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_cd_claims_cd_entities_id",
                        column: x => x.id,
                        principalSchema: "common",
                        principalTable: "cd_entities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cd_user_claims",
                schema: "admin",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    f_user = table.Column<Guid>(nullable: false),
                    f_claim = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cd_user_claims", x => new { x.f_user, x.f_claim });
                    table.ForeignKey(
                        name: "FK_cd_user_claims_cd_claims_f_claim",
                        column: x => x.f_claim,
                        principalSchema: "admin",
                        principalTable: "cd_claims",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_cd_user_claims_cd_claims_f_user",
                        column: x => x.f_user,
                        principalSchema: "admin",
                        principalTable: "cd_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cd_user_claims_f_claim",
                schema: "admin",
                table: "cd_user_claims",
                column: "f_claim");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cd_user_claims",
                schema: "admin");

            migrationBuilder.DropTable(
                name: "cd_claims",
                schema: "admin");
        }
    }
}
