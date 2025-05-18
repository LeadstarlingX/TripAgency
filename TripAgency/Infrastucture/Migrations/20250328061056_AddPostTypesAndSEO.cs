using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddPostTypesAndSEO : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Pph",
                table: "Cars",
                newName: "pph");

            migrationBuilder.AlterColumn<decimal>(
                name: "pph",
                table: "Cars",
                type: "decimal(12,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 28, 9, 10, 55, 979, DateTimeKind.Local).AddTicks(3874),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 26, 12, 58, 17, 671, DateTimeKind.Local).AddTicks(9931));

            migrationBuilder.CreateTable(
                name: "PostTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(256)", nullable: true, defaultValue: "")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    image = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    body = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    slug = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    views = table.Column<long>(type: "bigint", nullable: false, defaultValue: 0L),
                    status = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    publish_date = table.Column<DateTime>(type: "datetime2(7)", nullable: false, defaultValue: new DateTime(2025, 3, 28, 9, 10, 55, 985, DateTimeKind.Local).AddTicks(6239)),
                    sammary = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    author_id = table.Column<long>(type: "bigint", nullable: false),
                    post_type_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.id);
                    table.ForeignKey(
                        name: "FK_Posts_Employees_author_id",
                        column: x => x.author_id,
                        principalTable: "Employees",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Posts_PostTypes_post_type_id",
                        column: x => x.post_type_id,
                        principalTable: "PostTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostTags",
                columns: table => new
                {
                    tag_id = table.Column<int>(type: "int", nullable: false),
                    post_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostTags", x => new { x.post_id, x.tag_id });
                    table.ForeignKey(
                        name: "FK_PostTags_Posts_post_id",
                        column: x => x.post_id,
                        principalTable: "Posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostTags_Tags_tag_id",
                        column: x => x.tag_id,
                        principalTable: "Tags",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SEOMetaDatas",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    title = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(256)", nullable: true, defaultValue: ""),
                    keywords = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    url_slug = table.Column<string>(type: "nvarchar(256)", nullable: false),
                    post_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SEOMetaDatas", x => x.id);
                    table.ForeignKey(
                        name: "FK_SEOMetaDatas_Posts_post_id",
                        column: x => x.post_id,
                        principalTable: "Posts",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Posts_author_id",
                table: "Posts",
                column: "author_id");

            migrationBuilder.CreateIndex(
                name: "IX_Posts_post_type_id",
                table: "Posts",
                column: "post_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_PostTags_tag_id",
                table: "PostTags",
                column: "tag_id");

            migrationBuilder.CreateIndex(
                name: "IX_SEOMetaDatas_post_id",
                table: "SEOMetaDatas",
                column: "post_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostTags");

            migrationBuilder.DropTable(
                name: "SEOMetaDatas");

            migrationBuilder.DropTable(
                name: "Posts");

            migrationBuilder.DropTable(
                name: "PostTypes");

            migrationBuilder.RenameColumn(
                name: "pph",
                table: "Cars",
                newName: "Pph");

            migrationBuilder.AlterColumn<decimal>(
                name: "Pph",
                table: "Cars",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(12,2)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "start_date_time",
                table: "Bookings",
                type: "datetime2(7)",
                nullable: false,
                defaultValue: new DateTime(2025, 3, 26, 12, 58, 17, 671, DateTimeKind.Local).AddTicks(9931),
                oldClrType: typeof(DateTime),
                oldType: "datetime2(7)",
                oldDefaultValue: new DateTime(2025, 3, 28, 9, 10, 55, 979, DateTimeKind.Local).AddTicks(3874));
        }
    }
}
