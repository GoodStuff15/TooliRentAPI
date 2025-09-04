using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class testseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Borrowers_BorrowerId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_ToolType_Categories_CategoryId",
                table: "ToolType");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ToolType",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BorrowerId",
                table: "Bookings",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Borrowers_BorrowerId",
                table: "Bookings",
                column: "BorrowerId",
                principalTable: "Borrowers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ToolType_Categories_CategoryId",
                table: "ToolType",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Borrowers_BorrowerId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_ToolType_Categories_CategoryId",
                table: "ToolType");

            migrationBuilder.AlterColumn<int>(
                name: "CategoryId",
                table: "ToolType",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BorrowerId",
                table: "Bookings",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Borrowers_BorrowerId",
                table: "Bookings",
                column: "BorrowerId",
                principalTable: "Borrowers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ToolType_Categories_CategoryId",
                table: "ToolType",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
