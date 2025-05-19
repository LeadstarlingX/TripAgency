using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddTripPlanCars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 23, 10, 41, 6, 533, DateTimeKind.Local).AddTicks(9268),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 23, 10, 28, 20, 129, DateTimeKind.Local).AddTicks(4494));

            migrationBuilder.CreateTable(
                name: "TripPlanCars",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    price = table.Column<decimal>(type: "decimal(12,2)", nullable: false),
                    trip_plan_id = table.Column<int>(type: "int", nullable: false),
                    car_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripPlanCars", x => x.id);
                    table.ForeignKey(
                        name: "FK_TripPlanCars_Cars_car_id",
                        column: x => x.car_id,
                        principalTable: "Cars",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripPlanCars_TripPlans_trip_plan_id",
                        column: x => x.trip_plan_id,
                        principalTable: "TripPlans",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TripPlanCars_car_id_trip_plan_id",
                table: "TripPlanCars",
                columns: new[] { "car_id", "trip_plan_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_TripPlanCars_trip_plan_id",
                table: "TripPlanCars",
                column: "trip_plan_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripPlanCars");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 23, 10, 28, 20, 129, DateTimeKind.Local).AddTicks(4494),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 23, 10, 41, 6, 533, DateTimeKind.Local).AddTicks(9268));
        }
    }
}
