using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class MakeEmpIdInBookingNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Employees_employee_id",
                table: "Bookings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "publish_date",
                table: "Posts",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 6, 12, 13, 57, 17, 873, DateTimeKind.Local).AddTicks(4457),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 5, 27, 6, 59, 1, 892, DateTimeKind.Local).AddTicks(6236));

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 6, 12, 13, 57, 17, 868, DateTimeKind.Local).AddTicks(6693),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 5, 27, 6, 59, 1, 887, DateTimeKind.Local).AddTicks(3145));

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Employees_employee_id",
                table: "Bookings",
                column: "employee_id",
                principalTable: "Employees",
                principalColumn: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Employees_employee_id",
                table: "Bookings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "publish_date",
                table: "Posts",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 27, 6, 59, 1, 892, DateTimeKind.Local).AddTicks(6236),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 6, 12, 13, 57, 17, 873, DateTimeKind.Local).AddTicks(4457));

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 27, 6, 59, 1, 887, DateTimeKind.Local).AddTicks(3145),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 6, 12, 13, 57, 17, 868, DateTimeKind.Local).AddTicks(6693));

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Employees_employee_id",
                table: "Bookings",
                column: "employee_id",
                principalTable: "Employees",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
