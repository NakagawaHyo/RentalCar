using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentalCar.Migrations
{
    /// <inheritdoc />
    public partial class AddCarClass2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarClass_CarCategory_CarCategoryId",
                table: "CarClass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarClass",
                table: "CarClass");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarCategory",
                table: "CarCategory");

            migrationBuilder.RenameTable(
                name: "CarClass",
                newName: "CarClasses");

            migrationBuilder.RenameTable(
                name: "CarCategory",
                newName: "CarCategories");

            migrationBuilder.RenameIndex(
                name: "IX_CarClass_CarCategoryId",
                table: "CarClasses",
                newName: "IX_CarClasses_CarCategoryId");

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "CarClasses",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarClasses",
                table: "CarClasses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarCategories",
                table: "CarCategories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarClasses_CarCategories_CarCategoryId",
                table: "CarClasses",
                column: "CarCategoryId",
                principalTable: "CarCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarClasses_CarCategories_CarCategoryId",
                table: "CarClasses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarClasses",
                table: "CarClasses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CarCategories",
                table: "CarCategories");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "CarClasses");

            migrationBuilder.RenameTable(
                name: "CarClasses",
                newName: "CarClass");

            migrationBuilder.RenameTable(
                name: "CarCategories",
                newName: "CarCategory");

            migrationBuilder.RenameIndex(
                name: "IX_CarClasses_CarCategoryId",
                table: "CarClass",
                newName: "IX_CarClass_CarCategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarClass",
                table: "CarClass",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CarCategory",
                table: "CarCategory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CarClass_CarCategory_CarCategoryId",
                table: "CarClass",
                column: "CarCategoryId",
                principalTable: "CarCategory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
