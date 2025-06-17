using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class FixCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "publish_date",
                table: "Posts",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 6, 16, 21, 53, 27, 83, DateTimeKind.Local).AddTicks(5904),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 6, 16, 20, 44, 5, 153, DateTimeKind.Local).AddTicks(4454));

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Available",
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldDefaultValue: "Available");

            migrationBuilder.AlterColumn<string>(
                name: "model",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.AlterColumn<string>(
                name: "image",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 6, 16, 21, 53, 27, 72, DateTimeKind.Local).AddTicks(9328),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 6, 16, 20, 44, 5, 147, DateTimeKind.Local).AddTicks(8903));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "publish_date",
                table: "Posts",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 6, 16, 20, 44, 5, 153, DateTimeKind.Local).AddTicks(4454),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 6, 16, 21, 53, 27, 83, DateTimeKind.Local).AddTicks(5904));

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "Cars",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "Available",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldDefaultValue: "Available");

            migrationBuilder.AlterColumn<string>(
                name: "model",
                table: "Cars",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "image",
                table: "Cars",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 6, 16, 20, 44, 5, 147, DateTimeKind.Local).AddTicks(8903),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 6, 16, 21, 53, 27, 72, DateTimeKind.Local).AddTicks(9328));
        }
    }
}
