using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class UniqueUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_cd_users_c_username",
                schema: "admin",
                table: "cd_users",
                column: "c_username",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_cd_users_c_username",
                schema: "admin",
                table: "cd_users");
        }
    }
}
