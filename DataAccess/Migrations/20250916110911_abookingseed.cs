using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class abookingseed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "BorrowerId", "CreatedAt", "EndDate", "IsActive", "IsCancelled", "IsCompleted", "LateFeeId", "PickedUpDate", "ReturnedDate", "StartDate", "WasPickedUp", "WasReturned" },
                values: new object[] { 2, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new DateOnly(2025, 9, 14), true, false, false, null, null, null, new DateOnly(2025, 9, 10), true, false });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Bookings",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
