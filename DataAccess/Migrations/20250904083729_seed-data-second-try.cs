using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seeddatasecondtry : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_ToolType_ToolTypeId",
                table: "Tools");

            migrationBuilder.DropForeignKey(
                name: "FK_ToolType_Categories_CategoryId",
                table: "ToolType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToolType",
                table: "ToolType");

            migrationBuilder.RenameTable(
                name: "ToolType",
                newName: "ToolTypes");

            migrationBuilder.RenameIndex(
                name: "IX_ToolType_CategoryId",
                table: "ToolTypes",
                newName: "IX_ToolTypes_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToolTypes",
                table: "ToolTypes",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_ToolTypes_ToolTypeId",
                table: "Tools",
                column: "ToolTypeId",
                principalTable: "ToolTypes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ToolTypes_Categories_CategoryId",
                table: "ToolTypes",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Tools_ToolTypes_ToolTypeId",
                table: "Tools");

            migrationBuilder.DropForeignKey(
                name: "FK_ToolTypes_Categories_CategoryId",
                table: "ToolTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ToolTypes",
                table: "ToolTypes");

            migrationBuilder.RenameTable(
                name: "ToolTypes",
                newName: "ToolType");

            migrationBuilder.RenameIndex(
                name: "IX_ToolTypes_CategoryId",
                table: "ToolType",
                newName: "IX_ToolType_CategoryId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ToolType",
                table: "ToolType",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Tools_ToolType_ToolTypeId",
                table: "Tools",
                column: "ToolTypeId",
                principalTable: "ToolType",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ToolType_Categories_CategoryId",
                table: "ToolType",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
