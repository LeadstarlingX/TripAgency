using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class CheckUpdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "publish_date",
                table: "Posts",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 18, 9, 55, 33, 308, DateTimeKind.Local).AddTicks(9442),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 28, 9, 13, 48, 74, DateTimeKind.Local).AddTicks(1433));

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 18, 9, 55, 33, 296, DateTimeKind.Local).AddTicks(5778),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 28, 9, 13, 48, 67, DateTimeKind.Local).AddTicks(8536));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "publish_date",
                table: "Posts",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 28, 9, 13, 48, 74, DateTimeKind.Local).AddTicks(1433),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 4, 18, 9, 55, 33, 308, DateTimeKind.Local).AddTicks(9442));

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 28, 9, 13, 48, 67, DateTimeKind.Local).AddTicks(8536),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 4, 18, 9, 55, 33, 296, DateTimeKind.Local).AddTicks(5778));
        }
    }
}
