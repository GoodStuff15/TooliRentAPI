using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class tooltypeadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_Categories_CategoryId",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "MaxLoanDays",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "MinLoanDays",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "Tools",
                newName: "ToolTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_Tools_CategoryId",
                table: "Tools",
                newName: "IX_Tools_ToolTypeId");

            migrationBuilder.CreateTable(
                name: "ToolType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MaxLoanDays = table.Column<int>(type: "int", nullable: false),
                    MinLoanDays = table.Column<int>(type: "int", nullable: false),
                    CategoryId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolType", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToolType_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ToolType_CategoryId",
                table: "ToolType",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_ToolType_ToolTypeId",
                table: "Tools",
                column: "ToolTypeId",
                principalTable: "ToolType",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_ToolType_ToolTypeId",
                table: "Tools");

            migrationBuilder.DropTable(
                name: "ToolType");

            migrationBuilder.RenameColumn(
                name: "ToolTypeId",
                table: "Tools",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_Tools_ToolTypeId",
                table: "Tools",
                newName: "IX_Tools_CategoryId");

            migrationBuilder.AddColumn<int>(
                name: "MaxLoanDays",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MinLoanDays",
                table: "Categories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_Categories_CategoryId",
                table: "Tools",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }
    }
}
