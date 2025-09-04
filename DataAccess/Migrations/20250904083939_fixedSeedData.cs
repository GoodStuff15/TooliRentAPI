using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class fixedSeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "DelayPrice", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 15.00m, "Electric and battery-powered tools for construction and repair.", "Power Tools" },
                    { 2, 5.00m, "Manual tools for everyday tasks and repairs.", "Hand Tools" },
                    { 3, 8.50m, "Tools for gardening and landscaping.", "Garden Tools" }
                });

            migrationBuilder.InsertData(
                table: "ToolTypes",
                columns: new[] { "Id", "CategoryId", "MaxLoanDays", "MinLoanDays", "Name" },
                values: new object[,]
                {
                    { 1, 1, 7, 1, "Cordless Drill" },
                    { 2, 1, 5, 1, "Circular Saw" },
                    { 3, 1, 4, 1, "Angle Grinder" },
                    { 4, 2, 10, 1, "Hammer" },
                    { 5, 2, 10, 1, "Screwdriver Set" },
                    { 6, 2, 8, 1, "Wrench" },
                    { 7, 3, 3, 1, "Lawn Mower" },
                    { 8, 3, 3, 1, "Hedge Trimmer" },
                    { 9, 3, 7, 1, "Shovel" },
                    { 10, 3, 7, 1, "Rake" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ToolTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ToolTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ToolTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ToolTypes",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ToolTypes",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ToolTypes",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ToolTypes",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ToolTypes",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ToolTypes",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "ToolTypes",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Categories",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
