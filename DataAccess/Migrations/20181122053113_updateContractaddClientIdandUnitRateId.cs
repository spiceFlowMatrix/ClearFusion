using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class updateContractaddClientIdandUnitRateId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "ClientId",
                table: "ContractDetails",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "UnitRateId",
                table: "ContractDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_ClientId",
                table: "ContractDetails",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_ContractDetails_UnitRateId",
                table: "ContractDetails",
                column: "UnitRateId");

            migrationBuilder.AddForeignKey(
                name: "FK_ContractDetails_ClientDetails_ClientId",
                table: "ContractDetails",
                column: "ClientId",
                principalTable: "ClientDetails",
                principalColumn: "ClientId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ContractDetails_UnitRates_UnitRateId",
                table: "ContractDetails",
                column: "UnitRateId",
                principalTable: "UnitRates",
                principalColumn: "UnitRateId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContractDetails_ClientDetails_ClientId",
                table: "ContractDetails");

            migrationBuilder.DropForeignKey(
                name: "FK_ContractDetails_UnitRates_UnitRateId",
                table: "ContractDetails");

            migrationBuilder.DropIndex(
                name: "IX_ContractDetails_ClientId",
                table: "ContractDetails");

            migrationBuilder.DropIndex(
                name: "IX_ContractDetails_UnitRateId",
                table: "ContractDetails");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "ContractDetails");

            migrationBuilder.DropColumn(
                name: "UnitRateId",
                table: "ContractDetails");
        }
    }
}
