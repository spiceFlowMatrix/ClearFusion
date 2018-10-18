using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class orderTableModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IssedToLocation",
                table: "StorePurchaseOrders",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "IssueVoucherNo",
                table: "StorePurchaseOrders",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<long>(
                name: "Project",
                table: "StorePurchaseOrders",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "StorePurchaseOrders",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StatusAtTimeOfIssue",
                table: "StorePurchaseOrders",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IssedToLocation",
                table: "StorePurchaseOrders");

            migrationBuilder.DropColumn(
                name: "IssueVoucherNo",
                table: "StorePurchaseOrders");

            migrationBuilder.DropColumn(
                name: "Project",
                table: "StorePurchaseOrders");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "StorePurchaseOrders");

            migrationBuilder.DropColumn(
                name: "StatusAtTimeOfIssue",
                table: "StorePurchaseOrders");
        }
    }
}
