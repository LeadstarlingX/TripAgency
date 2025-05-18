using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class FixPMAndAddCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PaymentTransactions_paymentId",
                table: "PaymentTransactions");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "PaymentMethods",
                newName: "id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 22, 11, 43, 43, 277, DateTimeKind.Local).AddTicks(6295),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 22, 11, 27, 28, 384, DateTimeKind.Local).AddTicks(9965));

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Car",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Capacity = table.Column<int>(type: "int", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: false),
                    Pph = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Ppd = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Mbw = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Car", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Car_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_paymentId_paymentMethodId",
                table: "PaymentTransactions",
                columns: new[] { "paymentId", "paymentMethodId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Car_CategoryId",
                table: "Car",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_title",
                table: "Categories",
                column: "title",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Car");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransactions_paymentId_paymentMethodId",
                table: "PaymentTransactions");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "PaymentMethods",
                newName: "Id");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 22, 11, 27, 28, 384, DateTimeKind.Local).AddTicks(9965),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 22, 11, 43, 43, 277, DateTimeKind.Local).AddTicks(6295));

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_paymentId",
                table: "PaymentTransactions",
                column: "paymentId");
        }
    }
}
