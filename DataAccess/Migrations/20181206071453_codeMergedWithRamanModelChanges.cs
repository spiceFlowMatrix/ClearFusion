using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class codeMergedWithRamanModelChanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StoreItemPurchases_ProjectBudgetLine_BudgetLineId",
                table: "StoreItemPurchases");

            migrationBuilder.DropIndex(
                name: "IX_StoreItemPurchases_BudgetLineId",
                table: "StoreItemPurchases");

            migrationBuilder.AddColumn<bool>(
                name: "IsPurchaseVerified",
                table: "StoreItemPurchases",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "JournalCode",
                table: "StoreItemPurchases",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                table: "StoreItemPurchases",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PaymentTypeId",
                table: "StoreItemPurchases",
                nullable: true);

            migrationBuilder.AddColumn<long>(
                name: "VerifiedPurchaseVoucher",
                table: "StoreItemPurchases",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TinNumber",
                table: "EmployeeProfessionalDetail",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "EmployeeHistoryOutsideOrganization",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Position",
                table: "EmployeeHistoryOutsideCountry",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentTypes",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    PaymentId = table.Column<int>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Name = table.Column<string>(nullable: true),
                    AccountId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentTypes", x => x.PaymentId);
                    table.ForeignKey(
                        name: "FK_PaymentTypes_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PaymentTypes_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTypes_CreatedById",
                table: "PaymentTypes",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTypes_ModifiedById",
                table: "PaymentTypes",
                column: "ModifiedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentTypes");

            migrationBuilder.DropColumn(
                name: "IsPurchaseVerified",
                table: "StoreItemPurchases");

            migrationBuilder.DropColumn(
                name: "JournalCode",
                table: "StoreItemPurchases");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                table: "StoreItemPurchases");

            migrationBuilder.DropColumn(
                name: "PaymentTypeId",
                table: "StoreItemPurchases");

            migrationBuilder.DropColumn(
                name: "VerifiedPurchaseVoucher",
                table: "StoreItemPurchases");

            migrationBuilder.DropColumn(
                name: "TinNumber",
                table: "EmployeeProfessionalDetail");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "EmployeeHistoryOutsideOrganization");

            migrationBuilder.DropColumn(
                name: "Position",
                table: "EmployeeHistoryOutsideCountry");

            migrationBuilder.CreateIndex(
                name: "IX_StoreItemPurchases_BudgetLineId",
                table: "StoreItemPurchases",
                column: "BudgetLineId");

            migrationBuilder.AddForeignKey(
                name: "FK_StoreItemPurchases_ProjectBudgetLine_BudgetLineId",
                table: "StoreItemPurchases",
                column: "BudgetLineId",
                principalTable: "ProjectBudgetLine",
                principalColumn: "BudgetLineId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
