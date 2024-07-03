using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginSystem.Migrations
{
    public partial class Makingnullableforactivationcode : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ActivationCode",
                table: "UserInfos",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "UserId",
                keyValue: new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"),
                column: "ExpirationDate",
                value: new DateTime(2024, 7, 4, 11, 10, 18, 958, DateTimeKind.Local).AddTicks(1723));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "ActivationCode",
                table: "UserInfos",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "UserId",
                keyValue: new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"),
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 24, 20, 33, 7, 420, DateTimeKind.Local).AddTicks(9401));
        }
    }
}
