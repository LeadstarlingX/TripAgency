using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class FixValueInCustomerContactTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "country",
                table: "CustomerContacts",
                newName: "Value");

            migrationBuilder.AlterColumn<DateTime>(
                name: "publish_date",
                table: "Posts",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 26, 20, 32, 50, 599, DateTimeKind.Local).AddTicks(6561),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 4, 18, 9, 55, 33, 308, DateTimeKind.Local).AddTicks(9442));

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "CustomerContacts",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 5, 26, 20, 32, 50, 594, DateTimeKind.Local).AddTicks(5929),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 4, 18, 9, 55, 33, 296, DateTimeKind.Local).AddTicks(5778));

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_Value_customer_id_contact_type_id",
                table: "CustomerContacts",
                columns: new[] { "Value", "customer_id", "contact_type_id" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CustomerContacts_Value_customer_id_contact_type_id",
                table: "CustomerContacts");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "CustomerContacts",
                newName: "country");

            migrationBuilder.AlterColumn<DateTime>(
                name: "publish_date",
                table: "Posts",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 18, 9, 55, 33, 308, DateTimeKind.Local).AddTicks(9442),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 5, 26, 20, 32, 50, 599, DateTimeKind.Local).AddTicks(6561));

            migrationBuilder.AlterColumn<string>(
                name: "country",
                table: "CustomerContacts",
                type: "nvarchar(10)",
                maxLength: 256,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 4, 18, 9, 55, 33, 296, DateTimeKind.Local).AddTicks(5778),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 5, 26, 20, 32, 50, 594, DateTimeKind.Local).AddTicks(5929));
        }
    }
}
