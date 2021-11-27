﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace server.Migrations
{
    public partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "admin");

            migrationBuilder.EnsureSchema(
                name: "common");

            migrationBuilder.CreateTable(
                name: "cd_roles",
                schema: "admin",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    c_title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cs_entity_types",
                schema: "admin",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    c_name = table.Column<string>(nullable: false),
                    c_slug = table.Column<string>(nullable: false),
                    c_schema = table.Column<string>(nullable: false),
                    c_tablename = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cs_entity_types", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cd_claims",
                schema: "admin",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    f_type = table.Column<Guid>(nullable: false),
                    f_role = table.Column<Guid>(nullable: false),
                    b_create = table.Column<bool>(nullable: false, defaultValue: false),
                    b_read = table.Column<bool>(nullable: false, defaultValue: false),
                    b_update = table.Column<bool>(nullable: false, defaultValue: false),
                    b_delete = table.Column<bool>(nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_cd_claims_cd_roles_f_role",
                        column: x => x.f_role,
                        principalSchema: "admin",
                        principalTable: "cd_roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_cd_claims_cs_entity_types_f_type",
                        column: x => x.f_type,
                        principalSchema: "admin",
                        principalTable: "cs_entity_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cd_entities",
                schema: "common",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    f_type = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_entities", x => x.id);
                    table.ForeignKey(
                        name: "fk_cd_entities_cs_entitie_types_id",
                        column: x => x.f_type,
                        principalSchema: "admin",
                        principalTable: "cs_entity_types",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "cd_users",
                schema: "admin",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    c_username = table.Column<string>(nullable: false),
                    c_password = table.Column<string>(nullable: false),
                    c_refresh_token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cd_users", x => x.id);
                    table.ForeignKey(
                        name: "fk_cd_users_cd_entities_id",
                        column: x => x.id,
                        principalSchema: "common",
                        principalTable: "cd_entities",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.CreateTable(
                name: "cd_user_roles",
                schema: "admin",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    f_user = table.Column<Guid>(nullable: false),
                    f_role = table.Column<Guid>(nullable: false)
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
                name: "IX_cd_claims_f_role",
                schema: "admin",
                table: "cd_claims",
                column: "f_role");

            migrationBuilder.CreateIndex(
                name: "IX_cd_claims_f_type",
                schema: "admin",
                table: "cd_claims",
                column: "f_type");

            migrationBuilder.CreateIndex(
                name: "IX_cd_user_roles_f_role",
                schema: "admin",
                table: "cd_user_roles",
                column: "f_role");

            migrationBuilder.CreateIndex(
                name: "IX_cd_users_c_username",
                schema: "admin",
                table: "cd_users",
                column: "c_username",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_cd_entities_f_type",
                schema: "common",
                table: "cd_entities",
                column: "f_type");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cd_claims",
                schema: "admin");

            migrationBuilder.DropTable(
                name: "cd_user_roles",
                schema: "admin");

            migrationBuilder.DropTable(
                name: "cd_persons",
                schema: "common");

            migrationBuilder.DropTable(
                name: "cd_roles",
                schema: "admin");

            migrationBuilder.DropTable(
                name: "cd_users",
                schema: "admin");

            migrationBuilder.DropTable(
                name: "cd_entities",
                schema: "common");

            migrationBuilder.DropTable(
                name: "cs_entity_types",
                schema: "admin");
        }
    }
}