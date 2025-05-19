using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddTrip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripPlans_Trip_tripId",
                table: "TripPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trip",
                table: "Trip");

            migrationBuilder.RenameTable(
                name: "Trip",
                newName: "Trips");

            migrationBuilder.RenameColumn(
                name: "Slug",
                table: "Trips",
                newName: "slug");

            migrationBuilder.RenameColumn(
                name: "IsPrivate",
                table: "Trips",
                newName: "isPrivate");

            migrationBuilder.RenameColumn(
                name: "IsAvailable",
                table: "Trips",
                newName: "isAvailable");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Trips",
                newName: "description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Trips",
                newName: "id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 23, 11, 5, 58, 215, DateTimeKind.Local).AddTicks(2205),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 23, 10, 41, 6, 533, DateTimeKind.Local).AddTicks(9268));

            migrationBuilder.AlterColumn<string>(
                name: "slug",
                table: "Trips",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Trips",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Trips",
                type: "nvarchar(10)",
                maxLength: 50,
                nullable: false,
                defaultValue: "Trip");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trips",
                table: "Trips",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_TripPlans_Trips_tripId",
                table: "TripPlans",
                column: "tripId",
                principalTable: "Trips",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TripPlans_Trips_tripId",
                table: "TripPlans");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Trips",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "name",
                table: "Trips");

            migrationBuilder.RenameTable(
                name: "Trips",
                newName: "Trip");

            migrationBuilder.RenameColumn(
                name: "slug",
                table: "Trip",
                newName: "Slug");

            migrationBuilder.RenameColumn(
                name: "isPrivate",
                table: "Trip",
                newName: "IsPrivate");

            migrationBuilder.RenameColumn(
                name: "isAvailable",
                table: "Trip",
                newName: "IsAvailable");

            migrationBuilder.RenameColumn(
                name: "description",
                table: "Trip",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Trip",
                newName: "Id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 23, 10, 41, 6, 533, DateTimeKind.Local).AddTicks(9268),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 23, 11, 5, 58, 215, DateTimeKind.Local).AddTicks(2205));

            migrationBuilder.AlterColumn<string>(
                name: "Slug",
                table: "Trip",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Trip",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldDefaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Trip",
                table: "Trip",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TripPlans_Trip_tripId",
                table: "TripPlans",
                column: "tripId",
                principalTable: "Trip",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
