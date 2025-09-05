using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class toolseeddata : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Tools",
                columns: new[] { "Id", "BookingId", "Description", "IsAvailable", "Name", "ToolTypeId" },
                values: new object[,]
                {
                    { 1, null, "High-performance cordless drill for heavy-duty tasks.", true, "Makita Cordless Drill", 1 },
                    { 2, null, "Reliable cordless drill suitable for home and professional use.", true, "Bosch Power Drill", 1 },
                    { 3, null, "Precision circular saw for clean and accurate cuts.", true, "DeWalt Circular Saw", 2 },
                    { 4, null, "Lightweight circular saw ideal for quick jobs.", true, "Ryobi Circular Saw", 2 },
                    { 5, null, "Durable angle grinder for metal and masonry work.", true, "Milwaukee Angle Grinder", 3 },
                    { 6, null, "Compact angle grinder for detailed grinding tasks.", true, "Hitachi Angle Grinder", 3 },
                    { 7, null, "Classic claw hammer for carpentry and repairs.", true, "Stanley Claw Hammer", 4 },
                    { 8, null, "Heavy-duty framing hammer for construction projects.", true, "Estwing Framing Hammer", 4 },
                    { 9, null, "Precision screwdriver set for electronics and small repairs.", true, "Wiha Screwdriver Set", 5 },
                    { 10, null, "Versatile screwdriver set for household tasks.", true, "Craftsman Screwdriver Set", 5 },
                    { 11, null, "Adjustable wrench for plumbing and mechanical work.", true, "Klein Adjustable Wrench", 6 },
                    { 12, null, "Heavy-duty pipe wrench for tough jobs.", true, "Irwin Pipe Wrench", 6 },
                    { 13, null, "Efficient lawn mower for medium to large gardens.", true, "Honda Lawn Mower", 7 },
                    { 14, null, "Eco-friendly electric mower for quiet operation.", true, "Greenworks Electric Mower", 7 },
                    { 15, null, "Cordless hedge trimmer for easy garden maintenance.", true, "Black+Decker Hedge Trimmer", 8 },
                    { 16, null, "Professional hedge trimmer for precise cutting.", true, "Stihl Hedge Trimmer", 8 },
                    { 17, null, "Sturdy garden shovel for digging and planting.", true, "Fiskars Garden Shovel", 9 },
                    { 18, null, "Heavy-duty digging shovel for landscaping projects.", true, "Ames Digging Shovel", 9 },
                    { 19, null, "Wide leaf rake for efficient yard cleanup.", true, "True Temper Leaf Rake", 10 },
                    { 20, null, "Durable garden rake for soil preparation.", true, "Garant Garden Rake", 10 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 17);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 18);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 19);

            migrationBuilder.DeleteData(
                table: "Tools",
                keyColumn: "Id",
                keyValue: 20);
        }
    }
}
