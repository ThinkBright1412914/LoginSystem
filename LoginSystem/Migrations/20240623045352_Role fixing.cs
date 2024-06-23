using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginSystem.Migrations
{
    public partial class Rolefixing : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_UserInfos_UserInfoUserId",
                table: "Roles");

            migrationBuilder.DropIndex(
                name: "IX_Roles_UserInfoUserId",
                table: "Roles");

            migrationBuilder.DropColumn(
                name: "UserInfoUserId",
                table: "Roles");

            migrationBuilder.CreateTable(
                name: "RoleUserInfo",
                columns: table => new
                {
                    RolesRoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleUserInfo", x => new { x.RolesRoleId, x.UsersUserId });
                    table.ForeignKey(
                        name: "FK_RoleUserInfo_Roles_RolesRoleId",
                        column: x => x.RolesRoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RoleUserInfo_UserInfos_UsersUserId",
                        column: x => x.UsersUserId,
                        principalTable: "UserInfos",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "UserId",
                keyValue: new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"),
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 24, 10, 38, 52, 699, DateTimeKind.Local).AddTicks(24));

            migrationBuilder.CreateIndex(
                name: "IX_RoleUserInfo_UsersUserId",
                table: "RoleUserInfo",
                column: "UsersUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleUserInfo");

            migrationBuilder.AddColumn<Guid>(
                name: "UserInfoUserId",
                table: "Roles",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "UserId",
                keyValue: new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"),
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 24, 9, 36, 15, 557, DateTimeKind.Local).AddTicks(4608));

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UserInfoUserId",
                table: "Roles",
                column: "UserInfoUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_UserInfos_UserInfoUserId",
                table: "Roles",
                column: "UserInfoUserId",
                principalTable: "UserInfos",
                principalColumn: "UserId");
        }
    }
}
