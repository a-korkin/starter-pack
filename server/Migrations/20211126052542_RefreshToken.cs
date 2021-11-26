using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class RefreshToken : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "c_refresh_token",
                schema: "admin",
                table: "cd_users",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "c_refresh_token",
                schema: "admin",
                table: "cd_users");
        }
    }
}
