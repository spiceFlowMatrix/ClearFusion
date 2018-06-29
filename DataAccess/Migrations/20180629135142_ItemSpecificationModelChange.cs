using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class ItemSpecificationModelChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Value",
                table: "ItemSpecificationDetails",
                newName: "ItemSpecificationValue");

            migrationBuilder.AddColumn<string>(
                name: "ItemId",
                table: "ItemSpecificationDetails",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "ItemSpecificationDetails");

            migrationBuilder.RenameColumn(
                name: "ItemSpecificationValue",
                table: "ItemSpecificationDetails",
                newName: "Value");
        }
    }
}
