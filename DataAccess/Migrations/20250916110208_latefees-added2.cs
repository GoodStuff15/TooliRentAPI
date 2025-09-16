using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class latefeesadded2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LateFee_Bookings_BookingId1",
                table: "LateFee");

            migrationBuilder.DropForeignKey(
                name: "FK_LateFee_Borrowers_BorrowerId",
                table: "LateFee");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LateFee",
                table: "LateFee");

            migrationBuilder.RenameTable(
                name: "LateFee",
                newName: "LateFees");

            migrationBuilder.RenameIndex(
                name: "IX_LateFee_BorrowerId",
                table: "LateFees",
                newName: "IX_LateFees_BorrowerId");

            migrationBuilder.RenameIndex(
                name: "IX_LateFee_BookingId1",
                table: "LateFees",
                newName: "IX_LateFees_BookingId1");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "LateFees",
                type: "decimal(10,2)",
                precision: 10,
                scale: 2,
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LateFees",
                table: "LateFees",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LateFees_Bookings_BookingId1",
                table: "LateFees",
                column: "BookingId1",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LateFees_Borrowers_BorrowerId",
                table: "LateFees",
                column: "BorrowerId",
                principalTable: "Borrowers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LateFees_Bookings_BookingId1",
                table: "LateFees");

            migrationBuilder.DropForeignKey(
                name: "FK_LateFees_Borrowers_BorrowerId",
                table: "LateFees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LateFees",
                table: "LateFees");

            migrationBuilder.RenameTable(
                name: "LateFees",
                newName: "LateFee");

            migrationBuilder.RenameIndex(
                name: "IX_LateFees_BorrowerId",
                table: "LateFee",
                newName: "IX_LateFee_BorrowerId");

            migrationBuilder.RenameIndex(
                name: "IX_LateFees_BookingId1",
                table: "LateFee",
                newName: "IX_LateFee_BookingId1");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "LateFee",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,2)",
                oldPrecision: 10,
                oldScale: 2);

            migrationBuilder.AddPrimaryKey(
                name: "PK_LateFee",
                table: "LateFee",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LateFee_Bookings_BookingId1",
                table: "LateFee",
                column: "BookingId1",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_LateFee_Borrowers_BorrowerId",
                table: "LateFee",
                column: "BorrowerId",
                principalTable: "Borrowers",
                principalColumn: "Id");
        }
    }
}
