using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AccountTypeRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_CategoryPopulator_AccountTypeId",
                table: "CategoryPopulator",
                column: "AccountTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryPopulator_AccountType_AccountTypeId",
                table: "CategoryPopulator",
                column: "AccountTypeId",
                principalTable: "AccountType",
                principalColumn: "AccountTypeId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryPopulator_AccountType_AccountTypeId",
                table: "CategoryPopulator");

            migrationBuilder.DropIndex(
                name: "IX_CategoryPopulator_AccountTypeId",
                table: "CategoryPopulator");
        }
    }
}
