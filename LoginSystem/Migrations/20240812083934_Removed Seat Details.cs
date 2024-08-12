using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LoginSystem.Migrations
{
    public partial class RemovedSeatDetails : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_SeatsDetails_SeatDetailsId",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "SeatsDetails");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_SeatDetailsId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "SeatDetailsId",
                table: "Bookings");

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "UserId",
                keyValue: new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"),
                column: "ExpirationDate",
                value: new DateTime(2024, 8, 13, 14, 24, 34, 145, DateTimeKind.Local).AddTicks(5744));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SeatDetailsId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "SeatsDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ShowId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    SeatNo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SeatsDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SeatsDetails_Shows_ShowId",
                        column: x => x.ShowId,
                        principalTable: "Shows",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SeatsDetails_UserInfos_UserId",
                        column: x => x.UserId,
                        principalTable: "UserInfos",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "UserInfos",
                keyColumn: "UserId",
                keyValue: new Guid("9d3a21ba-e76b-49e6-a24e-2cf9d1531994"),
                column: "ExpirationDate",
                value: new DateTime(2024, 8, 12, 9, 48, 25, 150, DateTimeKind.Local).AddTicks(356));

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_SeatDetailsId",
                table: "Bookings",
                column: "SeatDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatsDetails_ShowId",
                table: "SeatsDetails",
                column: "ShowId");

            migrationBuilder.CreateIndex(
                name: "IX_SeatsDetails_UserId",
                table: "SeatsDetails",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_SeatsDetails_SeatDetailsId",
                table: "Bookings",
                column: "SeatDetailsId",
                principalTable: "SeatsDetails",
                principalColumn: "Id");
        }
    }
}
