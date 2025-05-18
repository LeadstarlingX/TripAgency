using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddEmpAndConectCusToBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 26, 12, 58, 17, 671, DateTimeKind.Local).AddTicks(9931),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 25, 14, 46, 22, 920, DateTimeKind.Local).AddTicks(9219));

            migrationBuilder.AddColumn<long>(
                name: "customer_id",
                table: "Bookings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "employee_id",
                table: "Bookings",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    user_id = table.Column<long>(type: "bigint", nullable: false),
                    hier_date = table.Column<DateTime>(type: "datetime2(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.user_id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_customer_id",
                table: "Bookings",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_employee_id",
                table: "Bookings",
                column: "employee_id");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Customers_customer_id",
                table: "Bookings",
                column: "customer_id",
                principalTable: "Customers",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Employees_employee_id",
                table: "Bookings",
                column: "employee_id",
                principalTable: "Employees",
                principalColumn: "user_id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Customers_customer_id",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Employees_employee_id",
                table: "Bookings");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_customer_id",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_employee_id",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "customer_id",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "employee_id",
                table: "Bookings");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 25, 14, 46, 22, 920, DateTimeKind.Local).AddTicks(9219),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 26, 12, 58, 17, 671, DateTimeKind.Local).AddTicks(9931));
        }
    }
}
