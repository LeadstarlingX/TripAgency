using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class FixImageToBeWithCarBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageShots_Bookings_bookingId",
                table: "ImageShots");

            migrationBuilder.RenameColumn(
                name: "bookingId",
                table: "ImageShots",
                newName: "carBookingId");

            migrationBuilder.RenameIndex(
                name: "IX_ImageShots_bookingId",
                table: "ImageShots",
                newName: "IX_ImageShots_carBookingId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 23, 11, 17, 4, 139, DateTimeKind.Local).AddTicks(8096),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 23, 11, 5, 58, 215, DateTimeKind.Local).AddTicks(2205));

            migrationBuilder.AddForeignKey(
                name: "FK_ImageShots_CarBookings_carBookingId",
                table: "ImageShots",
                column: "carBookingId",
                principalTable: "CarBookings",
                principalColumn: "booking_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageShots_CarBookings_carBookingId",
                table: "ImageShots");

            migrationBuilder.RenameColumn(
                name: "carBookingId",
                table: "ImageShots",
                newName: "bookingId");

            migrationBuilder.RenameIndex(
                name: "IX_ImageShots_carBookingId",
                table: "ImageShots",
                newName: "IX_ImageShots_bookingId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 23, 11, 5, 58, 215, DateTimeKind.Local).AddTicks(2205),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 23, 11, 17, 4, 139, DateTimeKind.Local).AddTicks(8096));

            migrationBuilder.AddForeignKey(
                name: "FK_ImageShots_Bookings_bookingId",
                table: "ImageShots",
                column: "bookingId",
                principalTable: "Bookings",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
