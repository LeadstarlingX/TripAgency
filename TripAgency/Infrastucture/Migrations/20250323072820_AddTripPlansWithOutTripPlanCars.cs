using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddTripPlansWithOutTripPlanCars : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripPlans_Regions_RegionId",
                table: "TripPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_TripPlans_Trip_TripId",
                table: "TripPlans");

            migrationBuilder.RenameColumn(
                name: "TripId",
                table: "TripPlans",
                newName: "tripId");

            migrationBuilder.RenameColumn(
                name: "RegionId",
                table: "TripPlans",
                newName: "regionId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TripPlans",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_TripPlans_TripId",
                table: "TripPlans",
                newName: "IX_TripPlans_tripId");

            migrationBuilder.RenameIndex(
                name: "IX_TripPlans_RegionId",
                table: "TripPlans",
                newName: "IX_TripPlans_regionId");

            migrationBuilder.AddColumn<string>(
                name: "Stops",
                table: "TripPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "duration",
                table: "TripPlans",
                type: "decimal(5,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<DateTime>(
                name: "end_date_time",
                table: "TripPlans",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "hotels_stays",
                table: "TripPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "induded_services",
                table: "TripPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "meals_plan",
                table: "TripPlans",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "start_date_time",
                table: "TripPlans",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 23, 10, 28, 20, 129, DateTimeKind.Local).AddTicks(4494),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 23, 7, 25, 9, 957, DateTimeKind.Local).AddTicks(1679));

            migrationBuilder.AddForeignKey(
                name: "FK_TripPlans_Regions_regionId",
                table: "TripPlans",
                column: "regionId",
                principalTable: "Regions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripPlans_Trip_tripId",
                table: "TripPlans",
                column: "tripId",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripPlans_Regions_regionId",
                table: "TripPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_TripPlans_Trip_tripId",
                table: "TripPlans");

            migrationBuilder.DropColumn(
                name: "Stops",
                table: "TripPlans");

            migrationBuilder.DropColumn(
                name: "duration",
                table: "TripPlans");

            migrationBuilder.DropColumn(
                name: "end_date_time",
                table: "TripPlans");

            migrationBuilder.DropColumn(
                name: "hotels_stays",
                table: "TripPlans");

            migrationBuilder.DropColumn(
                name: "induded_services",
                table: "TripPlans");

            migrationBuilder.DropColumn(
                name: "meals_plan",
                table: "TripPlans");

            migrationBuilder.DropColumn(
                name: "start_date_time",
                table: "TripPlans");

            migrationBuilder.RenameColumn(
                name: "tripId",
                table: "TripPlans",
                newName: "TripId");

            migrationBuilder.RenameColumn(
                name: "regionId",
                table: "TripPlans",
                newName: "RegionId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "TripPlans",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_TripPlans_tripId",
                table: "TripPlans",
                newName: "IX_TripPlans_TripId");

            migrationBuilder.RenameIndex(
                name: "IX_TripPlans_regionId",
                table: "TripPlans",
                newName: "IX_TripPlans_RegionId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 23, 7, 25, 9, 957, DateTimeKind.Local).AddTicks(1679),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 23, 10, 28, 20, 129, DateTimeKind.Local).AddTicks(4494));

            migrationBuilder.AddForeignKey(
                name: "FK_TripPlans_Regions_RegionId",
                table: "TripPlans",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripPlans_Trip_TripId",
                table: "TripPlans",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
