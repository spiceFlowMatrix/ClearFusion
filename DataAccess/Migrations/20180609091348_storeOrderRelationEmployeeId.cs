using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class storeOrderRelationEmployeeId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorePurchaseOrders_EmployeeDetail_EmployeeId",
                table: "StorePurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_StorePurchaseOrders_EmployeeId",
                table: "StorePurchaseOrders");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "StorePurchaseOrders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReturnedDate",
                table: "StorePurchaseOrders",
                nullable: true,
                oldClrType: typeof(DateTime));

            migrationBuilder.CreateIndex(
                name: "IX_StorePurchaseOrders_IssuedToEmployeeId",
                table: "StorePurchaseOrders",
                column: "IssuedToEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_StorePurchaseOrders_EmployeeDetail_IssuedToEmployeeId",
                table: "StorePurchaseOrders",
                column: "IssuedToEmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StorePurchaseOrders_EmployeeDetail_IssuedToEmployeeId",
                table: "StorePurchaseOrders");

            migrationBuilder.DropIndex(
                name: "IX_StorePurchaseOrders_IssuedToEmployeeId",
                table: "StorePurchaseOrders");

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReturnedDate",
                table: "StorePurchaseOrders",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "EmployeeId",
                table: "StorePurchaseOrders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_StorePurchaseOrders_EmployeeId",
                table: "StorePurchaseOrders",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_StorePurchaseOrders_EmployeeDetail_EmployeeId",
                table: "StorePurchaseOrders",
                column: "EmployeeId",
                principalTable: "EmployeeDetail",
                principalColumn: "EmployeeID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
