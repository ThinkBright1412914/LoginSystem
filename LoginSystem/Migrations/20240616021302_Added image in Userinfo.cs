using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginSystem.Migrations
{
    public partial class AddedimageinUserinfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ImageFile",
                table: "UserInfos",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "UserId",
                keyValue: new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"),
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 17, 7, 58, 2, 616, DateTimeKind.Local).AddTicks(2318));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageFile",
                table: "UserInfos");

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "UserId",
                keyValue: new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"),
                column: "ExpirationDate",
                value: new DateTime(2024, 6, 8, 11, 23, 11, 456, DateTimeKind.Local).AddTicks(7711));
        }
    }
}
