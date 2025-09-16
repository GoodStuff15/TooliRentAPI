using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class latefeebookingidconflict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LateFees_Bookings_BookingId1",
                table: "LateFees");

            migrationBuilder.DropIndex(
                name: "IX_LateFees_BookingId1",
                table: "LateFees");

            migrationBuilder.DropColumn(
                name: "BookingId1",
                table: "LateFees");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BookingId1",
                table: "LateFees",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LateFees_BookingId1",
                table: "LateFees",
                column: "BookingId1");

            migrationBuilder.AddForeignKey(
                name: "FK_LateFees_Bookings_BookingId1",
                table: "LateFees",
                column: "BookingId1",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
