using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddRegionsAndIndexesToCBAndTBUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripBookings_TripPlan_tripPlanId",
                table: "TripBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_TripPlan_Region_RegionId",
                table: "TripPlan");

            migrationBuilder.DropForeignKey(
                name: "FK_TripPlan_Trip_TripId",
                table: "TripPlan");

            migrationBuilder.DropIndex(
                name: "IX_TripBookings_tripPlanId",
                table: "TripBookings");

            migrationBuilder.DropIndex(
                name: "IX_CarBookings_car_id",
                table: "CarBookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripPlan",
                table: "TripPlan");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Region",
                table: "Region");

            migrationBuilder.RenameTable(
                name: "TripPlan",
                newName: "TripPlans");

            migrationBuilder.RenameTable(
                name: "Region",
                newName: "Regions");

            migrationBuilder.RenameIndex(
                name: "IX_TripPlan_TripId",
                table: "TripPlans",
                newName: "IX_TripPlans_TripId");

            migrationBuilder.RenameIndex(
                name: "IX_TripPlan_RegionId",
                table: "TripPlans",
                newName: "IX_TripPlans_RegionId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Regions",
                newName: "name");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Regions",
                newName: "id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 23, 7, 25, 9, 957, DateTimeKind.Local).AddTicks(1679),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 23, 7, 15, 33, 340, DateTimeKind.Local).AddTicks(2658));

            migrationBuilder.AlterColumn<string>(
                name: "name",
                table: "Regions",
                type: "nvarchar(10)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripPlans",
                table: "TripPlans",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Regions",
                table: "Regions",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_TripBookings_tripPlanId_booking_id",
                table: "TripBookings",
                columns: new[] { "tripPlanId", "booking_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarBookings_car_id_booking_id",
                table: "CarBookings",
                columns: new[] { "car_id", "booking_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Regions_name",
                table: "Regions",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_TripBookings_TripPlans_tripPlanId",
                table: "TripBookings",
                column: "tripPlanId",
                principalTable: "TripPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripBookings_TripPlans_tripPlanId",
                table: "TripBookings");

            migrationBuilder.DropForeignKey(
                name: "FK_TripPlans_Regions_RegionId",
                table: "TripPlans");

            migrationBuilder.DropForeignKey(
                name: "FK_TripPlans_Trip_TripId",
                table: "TripPlans");

            migrationBuilder.DropIndex(
                name: "IX_TripBookings_tripPlanId_booking_id",
                table: "TripBookings");

            migrationBuilder.DropIndex(
                name: "IX_CarBookings_car_id_booking_id",
                table: "CarBookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TripPlans",
                table: "TripPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Regions",
                table: "Regions");

            migrationBuilder.DropIndex(
                name: "IX_Regions_name",
                table: "Regions");

            migrationBuilder.RenameTable(
                name: "TripPlans",
                newName: "TripPlan");

            migrationBuilder.RenameTable(
                name: "Regions",
                newName: "Region");

            migrationBuilder.RenameIndex(
                name: "IX_TripPlans_TripId",
                table: "TripPlan",
                newName: "IX_TripPlan_TripId");

            migrationBuilder.RenameIndex(
                name: "IX_TripPlans_RegionId",
                table: "TripPlan",
                newName: "IX_TripPlan_RegionId");

            migrationBuilder.RenameColumn(
                name: "name",
                table: "Region",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Region",
                newName: "Id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 23, 7, 15, 33, 340, DateTimeKind.Local).AddTicks(2658),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 23, 7, 25, 9, 957, DateTimeKind.Local).AddTicks(1679));

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Region",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TripPlan",
                table: "TripPlan",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Region",
                table: "Region",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TripBookings_tripPlanId",
                table: "TripBookings",
                column: "tripPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_CarBookings_car_id",
                table: "CarBookings",
                column: "car_id");

            migrationBuilder.AddForeignKey(
                name: "FK_TripBookings_TripPlan_tripPlanId",
                table: "TripBookings",
                column: "tripPlanId",
                principalTable: "TripPlan",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripPlan_Region_RegionId",
                table: "TripPlan",
                column: "RegionId",
                principalTable: "Region",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TripPlan_Trip_TripId",
                table: "TripPlan",
                column: "TripId",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
