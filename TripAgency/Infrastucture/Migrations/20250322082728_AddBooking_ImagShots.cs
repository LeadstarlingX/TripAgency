using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddBooking_ImagShots : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageShot_Bookings_BookingId",
                table: "ImageShot");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_PaymentMethods_PaymentMethodId",
                table: "PaymentTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Payments_PaymentId",
                table: "PaymentTransactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageShot",
                table: "ImageShot");

            migrationBuilder.RenameTable(
                name: "ImageShot",
                newName: "ImageShots");

            migrationBuilder.RenameColumn(
                name: "PaymentMethodId",
                table: "PaymentTransactions",
                newName: "paymentMethodId");

            migrationBuilder.RenameColumn(
                name: "PaymentId",
                table: "PaymentTransactions",
                newName: "paymentId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentTransactions_PaymentMethodId",
                table: "PaymentTransactions",
                newName: "IX_PaymentTransactions_paymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentTransactions_PaymentId",
                table: "PaymentTransactions",
                newName: "IX_PaymentTransactions_paymentId");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "Payments",
                newName: "bookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_BookingId",
                table: "Payments",
                newName: "IX_Payments_bookingId");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Bookings",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "NumOfPassengers",
                table: "Bookings",
                newName: "numOfPassengers");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Bookings",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "StartDateTime",
                table: "Bookings",
                newName: "start_date_time");

            migrationBuilder.RenameColumn(
                name: "EndDateTime",
                table: "Bookings",
                newName: "end_date_time");

            migrationBuilder.RenameColumn(
                name: "Type",
                table: "ImageShots",
                newName: "type");

            migrationBuilder.RenameColumn(
                name: "Path",
                table: "ImageShots",
                newName: "path");

            migrationBuilder.RenameColumn(
                name: "BookingId",
                table: "ImageShots",
                newName: "bookingId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ImageShots",
                newName: "id");

            migrationBuilder.RenameIndex(
                name: "IX_ImageShot_BookingId",
                table: "ImageShots",
                newName: "IX_ImageShots_bookingId");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "Bookings",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 22, 11, 27, 28, 384, DateTimeKind.Local).AddTicks(9965),
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<DateTime>(
                name: "end_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "ImageShots",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "path",
                table: "ImageShots",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageShots",
                table: "ImageShots",
                column: "id");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentMethods_method",
                table: "PaymentMethods",
                column: "method",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_ImageShots_Bookings_bookingId",
                table: "ImageShots",
                column: "bookingId",
                principalTable: "Bookings",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bookings_bookingId",
                table: "Payments",
                column: "bookingId",
                principalTable: "Bookings",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_PaymentMethods_paymentMethodId",
                table: "PaymentTransactions",
                column: "paymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Payments_paymentId",
                table: "PaymentTransactions",
                column: "paymentId",
                principalTable: "Payments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageShots_Bookings_bookingId",
                table: "ImageShots");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Bookings_bookingId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_PaymentMethods_paymentMethodId",
                table: "PaymentTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Payments_paymentId",
                table: "PaymentTransactions");

            migrationBuilder.DropIndex(
                name: "IX_PaymentMethods_method",
                table: "PaymentMethods");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageShots",
                table: "ImageShots");

            migrationBuilder.RenameTable(
                name: "ImageShots",
                newName: "ImageShot");

            migrationBuilder.RenameColumn(
                name: "paymentMethodId",
                table: "PaymentTransactions",
                newName: "PaymentMethodId");

            migrationBuilder.RenameColumn(
                name: "paymentId",
                table: "PaymentTransactions",
                newName: "PaymentId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentTransactions_paymentMethodId",
                table: "PaymentTransactions",
                newName: "IX_PaymentTransactions_PaymentMethodId");

            migrationBuilder.RenameIndex(
                name: "IX_PaymentTransactions_paymentId",
                table: "PaymentTransactions",
                newName: "IX_PaymentTransactions_PaymentId");

            migrationBuilder.RenameColumn(
                name: "bookingId",
                table: "Payments",
                newName: "BookingId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_bookingId",
                table: "Payments",
                newName: "IX_Payments_BookingId");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Bookings",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "numOfPassengers",
                table: "Bookings",
                newName: "NumOfPassengers");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Bookings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "start_date_time",
                table: "Bookings",
                newName: "StartDateTime");

            migrationBuilder.RenameColumn(
                name: "end_date_time",
                table: "Bookings",
                newName: "EndDateTime");

            migrationBuilder.RenameColumn(
                name: "type",
                table: "ImageShot",
                newName: "Type");

            migrationBuilder.RenameColumn(
                name: "path",
                table: "ImageShot",
                newName: "Path");

            migrationBuilder.RenameColumn(
                name: "bookingId",
                table: "ImageShot",
                newName: "BookingId");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "ImageShot",
                newName: "Id");

            migrationBuilder.RenameIndex(
                name: "IX_ImageShots_bookingId",
                table: "ImageShot",
                newName: "IX_ImageShot_BookingId");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "Bookings",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "StartDateTime",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 22, 11, 27, 28, 384, DateTimeKind.Local).AddTicks(9965));

            migrationBuilder.AlterColumn<DateTime>(
                name: "EndDateTime",
                table: "Bookings",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)");

            migrationBuilder.AlterColumn<string>(
                name: "Type",
                table: "ImageShot",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.AlterColumn<string>(
                name: "Path",
                table: "ImageShot",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageShot",
                table: "ImageShot",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageShot_Bookings_BookingId",
                table: "ImageShot",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Bookings_BookingId",
                table: "Payments",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_PaymentMethods_PaymentMethodId",
                table: "PaymentTransactions",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Payments_PaymentId",
                table: "PaymentTransactions",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
