using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginSystem.Migrations
{
    public partial class UsersRole : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserInfoUserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.RoleId);
                    table.ForeignKey(
                        name: "FK_Roles_UserInfos_UserInfoUserId",
                        column: x => x.UserInfoUserId,
                        principalTable: "UserInfos",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_UserInfos_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfos",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName", "UserInfoUserId" },
                values: new object[] { new Guid("115f896e-5a2a-4cf1-a90c-2bb3c01740dc"), "User", null });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "RoleId", "RoleName", "UserInfoUserId" },
                values: new object[] { new Guid("91fb10cd-b0ce-4e45-8763-6aaf1b8cb2f9"), "Admin", null });

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

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "UserId",
                keyValue: new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"),
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 17, 7, 58, 2, 616, DateTimeKind.Local).AddTicks(2318));
        }
    }
}
