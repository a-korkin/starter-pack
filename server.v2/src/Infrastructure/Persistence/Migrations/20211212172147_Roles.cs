using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Infrastructure.Persistence.Migrations
{
    public partial class Roles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cd_user_roles",
                schema: "admin",
                columns: table => new
                {
                    f_user = table.Column<Guid>(nullable: false),
                    f_role = table.Column<Guid>(nullable: false),
                    id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_user_claims", x => new { x.f_user, x.f_role });
                    table.ForeignKey(
                        name: "fk_cd_user_roles_cd_users_f_role",
                        column: x => x.f_role,
                        principalSchema: "admin",
                        principalTable: "cd_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cd_user_roles_cd_roles_f_user",
                        column: x => x.f_user,
                        principalSchema: "admin",
                        principalTable: "cd_users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_cd_user_roles_f_role",
                schema: "admin",
                table: "cd_user_roles",
                column: "f_role");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cd_user_roles",
                schema: "admin");
        }
    }
}
