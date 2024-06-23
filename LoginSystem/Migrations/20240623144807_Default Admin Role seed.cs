using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginSystem.Migrations
{
    public partial class DefaultAdminRoleseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "UserId",
                keyValue: new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"),
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 24, 20, 33, 7, 420, DateTimeKind.Local).AddTicks(9401));

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("91fb10cd-b0ce-4e45-8763-6aaf1b8cb2f9"), new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("91fb10cd-b0ce-4e45-8763-6aaf1b8cb2f9"), new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994") });

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "UserId",
                keyValue: new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"),
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 24, 10, 38, 52, 699, DateTimeKind.Local).AddTicks(24));
        }
    }
}
