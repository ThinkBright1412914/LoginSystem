using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginSystem.Migrations
{
    public partial class DefaultAdminUserseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "UserInfos",
                columns: new[] { "UserId", "ActivationCode", "Email", "ExpirationDate", "IsActive", "Password", "UserName" },
                values: new object[] { new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"), "678999", "nabinthekishor@gmail.com", new DateTime(2024, 6, 8, 11, 23, 11, 456, DateTimeKind.Local).AddTicks(7711), true, "I0FkbWluMTIz", "Admin" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserInfos",
                keyColumn: "UserId",
                keyValue: new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"));
        }
    }
}
