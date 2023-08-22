using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RentalCar.Migrations
{
    /// <inheritdoc />
    public partial class AddCarClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "car_categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_car_categories", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "car_classes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    car_category_id = table.Column<int>(type: "integer", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    remarks = table.Column<string>(type: "text", nullable: false),
                    display_order = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_car_classes", x => x.id);
                    table.ForeignKey(
                        name: "fk_car_classes_car_categories_car_category_id",
                        column: x => x.car_category_id,
                        principalTable: "car_categories",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "car_categories",
                columns: new[] { "id", "name" },
                values: new object[,]
                {
                    { 1, "乗用車" },
                    { 2, "軽自動車" },
                    { 3, "エコカー" },
                    { 4, "スポーツカー" },
                    { 5, "ミニバン・ワゴン" },
                    { 6, "SUV" },
                    { 7, "バン" },
                    { 8, "トラック" },
                    { 9, "バス" },
                    { 10, "その他" }
                });

            migrationBuilder.CreateIndex(
                name: "ix_car_classes_car_category_id",
                table: "car_classes",
                column: "car_category_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "car_classes");

            migrationBuilder.DropTable(
                name: "car_categories");
        }
    }
}
