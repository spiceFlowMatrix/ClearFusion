using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updatesalaryheadaccountaddition : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountNo",
                table: "SalaryHeadDetails",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<int>(
                name: "TransactionTypeId",
                table: "SalaryHeadDetails",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "SalaryHeadDetails");

            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "SalaryHeadDetails");
        }
    }
}
