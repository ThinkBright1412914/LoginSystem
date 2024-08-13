using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginSystem.Migrations
{
    public partial class AddedReservedSeatsinShow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReservedSeats",
                table: "Shows",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "UserId",
                keyValue: new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"),
                column: "ExpirationDate",
                value: new DateTime(2024, 8, 14, 20, 10, 18, 34, DateTimeKind.Local).AddTicks(3711));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReservedSeats",
                table: "Shows");

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "UserId",
                keyValue: new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"),
                column: "ExpirationDate",
                value: new DateTime(2024, 8, 13, 14, 26, 21, 829, DateTimeKind.Local).AddTicks(6077));
        }
    }
}
