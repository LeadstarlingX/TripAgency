using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddTagAndCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Car_Categories_CategoryId",
                table: "Car");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Car",
                table: "Car");

            migrationBuilder.RenameTable(
                name: "Car",
                newName: "Cars");

            migrationBuilder.RenameColumn(
                name: "Model",
                table: "Cars",
                newName: "model");

            migrationBuilder.RenameColumn(
                name: "Mbw",
                table: "Cars",
                newName: "mbw");

            migrationBuilder.RenameColumn(
                name: "Image",
                table: "Cars",
                newName: "image");

            migrationBuilder.RenameColumn(
                name: "Color",
                table: "Cars",
                newName: "color");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Cars",
                newName: "categoryId");

            migrationBuilder.RenameColumn(
                name: "Capacity",
                table: "Cars",
                newName: "capacity");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Cars",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "Ppd",
                table: "Cars",
                newName: "price_per_day");

            migrationBuilder.RenameIndex(
                name: "IX_Car_CategoryId",
                table: "Cars",
                newName: "IX_Cars_categoryId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 22, 12, 7, 18, 441, DateTimeKind.Local).AddTicks(7930),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 22, 11, 43, 43, 277, DateTimeKind.Local).AddTicks(6295));

            migrationBuilder.AlterColumn<string>(
                name: "model",
                table: "Cars",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "mbw",
                table: "Cars",
                type: "decimal(12,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<string>(
                name: "image",
                table: "Cars",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "color",
                table: "Cars",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<decimal>(
                name: "price_per_day",
                table: "Cars",
                type: "decimal(12,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cars",
                table: "Cars",
                column: "id");

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tags_name",
                table: "Tags",
                column: "name",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Categories_categoryId",
                table: "Cars",
                column: "categoryId",
                principalTable: "Categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Categories_categoryId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cars",
                table: "Cars");

            migrationBuilder.RenameTable(
                name: "Cars",
                newName: "Car");

            migrationBuilder.RenameColumn(
                name: "model",
                table: "Car",
                newName: "Model");

            migrationBuilder.RenameColumn(
                name: "mbw",
                table: "Car",
                newName: "Mbw");

            migrationBuilder.RenameColumn(
                name: "image",
                table: "Car",
                newName: "Image");

            migrationBuilder.RenameColumn(
                name: "color",
                table: "Car",
                newName: "Color");

            migrationBuilder.RenameColumn(
                name: "categoryId",
                table: "Car",
                newName: "CategoryId");

            migrationBuilder.RenameColumn(
                name: "capacity",
                table: "Car",
                newName: "Capacity");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Car",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "price_per_day",
                table: "Car",
                newName: "Ppd");

            migrationBuilder.RenameIndex(
                name: "IX_Cars_categoryId",
                table: "Car",
                newName: "IX_Car_CategoryId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 22, 11, 43, 43, 277, DateTimeKind.Local).AddTicks(6295),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 22, 12, 7, 18, 441, DateTimeKind.Local).AddTicks(7930));

            migrationBuilder.AlterColumn<string>(
                name: "Model",
                table: "Car",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Mbw",
                table: "Car",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,2)");

            migrationBuilder.AlterColumn<string>(
                name: "Image",
                table: "Car",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.AlterColumn<string>(
                name: "Color",
                table: "Car",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Ppd",
                table: "Car",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Car",
                table: "Car",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Car_Categories_CategoryId",
                table: "Car",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
