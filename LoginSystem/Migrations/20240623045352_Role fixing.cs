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

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "UserId",
                keyValue: new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"),
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 24, 10, 38, 52, 699, DateTimeKind.Local).AddTicks(24));

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
