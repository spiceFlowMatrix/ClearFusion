using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class UpdatedProjectLogisticsTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long[]>(
                name: "PurchaseId",
                table: "ProjectLogisticRequests",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VoucherNo",
                table: "ProjectLogisticRequests",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseId",
                table: "ProjectLogisticRequests");

            migrationBuilder.DropColumn(
                name: "VoucherNo",
                table: "ProjectLogisticRequests");
        }
    }
}
