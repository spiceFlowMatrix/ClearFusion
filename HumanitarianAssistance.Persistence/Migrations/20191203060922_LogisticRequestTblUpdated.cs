using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HumanitarianAssistance.Persistence.Migrations
{
    public partial class LogisticRequestTblUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "PurchaseDate",
                table: "ProjectLogisticRequests",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "LeaveReasonDetail",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PurchaseDate",
                table: "ProjectLogisticRequests");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "LeaveReasonDetail");
        }
    }
}
