using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addAccountAndTransactionTypeEmployeePayroll : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AccountNo",
                table: "EmployeePayroll",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TransactionTypeId",
                table: "EmployeePayroll",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AccountNo",
                table: "EmployeePayroll");

            migrationBuilder.DropColumn(
                name: "TransactionTypeId",
                table: "EmployeePayroll");
        }
    }
}
