using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class toolbookingsmany2many : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Bookings_BookingId",
                table: "Tools");

            migrationBuilder.DropIndex(
                name: "IX_Tools_BookingId",
                table: "Tools");

            migrationBuilder.CreateTable(
                name: "BookingTool",
                columns: table => new
                {
                    BookingsId = table.Column<int>(type: "int", nullable: false),
                    ToolsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingTool", x => new { x.BookingsId, x.ToolsId });
                    table.ForeignKey(
                        name: "FK_BookingTool_Bookings_BookingsId",
                        column: x => x.BookingsId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingTool_Tools_ToolsId",
                        column: x => x.ToolsId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Borrowers",
                columns: new[] { "Id", "Address", "Email", "FirstName", "IsActive", "LastName", "PhoneNumber", "UserId" },
                values: new object[] { 1, "Wayvay 8", "gustav@swedbonk.se", "Gustav", true, "Eriksson", "070881220", "9debe80a-df48-4525-94af-be01f484f601" });

            migrationBuilder.CreateIndex(
                name: "IX_BookingTool_ToolsId",
                table: "BookingTool",
                column: "ToolsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingTool");

            migrationBuilder.DeleteData(
                table: "Borrowers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.CreateIndex(
                name: "IX_Tools_BookingId",
                table: "Tools",
                column: "BookingId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Bookings_BookingId",
                table: "Tools",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");
        }
    }
}
