using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class addtableApproveRejectPermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ApproveRejectPermission",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    Id = table.Column<long>(type: "serial", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    PageId = table.Column<int>(nullable: false),
                    Approve = table.Column<bool>(nullable: false),
                    Reject = table.Column<bool>(nullable: false),
                    RoleId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ApproveRejectPermission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ApproveRejectPermission_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApproveRejectPermission_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ApproveRejectPermission_ApplicationPages_PageId",
                        column: x => x.PageId,
                        principalTable: "ApplicationPages",
                        principalColumn: "PageId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "ApplicationPages",
                columns: new[] { "PageId", "CreatedById", "CreatedDate", "IsDeleted", "ModifiedById", "ModifiedDate", "ModuleId", "ModuleName", "PageName" },
                values: new object[,]
                {
                    { 49, null, null, false, null, null, 6, "Marketing", "TimeCategory" },
                    { 69, null, null, false, null, null, 8, "Projects", "ProjectDetails" },
                    { 68, null, null, false, null, null, 8, "Projects", "Donors" },
                    { 67, null, null, false, null, null, 8, "Projects", "MyProjects" },
                    { 66, null, null, false, null, null, 6, "Marketing", "Contracts" },
                    { 65, null, null, false, null, null, 6, "Marketing", "Jobs" },
                    { 64, null, null, false, null, null, 6, "Marketing", "UnitRates" },
                    { 63, null, null, false, null, null, 6, "Marketing", "Clients" },
                    { 62, null, null, false, null, null, 7, "AccountingNew", "Vouchers" },
                    { 61, null, null, false, null, null, 7, "AccountingNew", "IncomeExpenseReport" },
                    { 70, null, null, false, null, null, 8, "Projects", "Proposal" },
                    { 60, null, null, false, null, null, 7, "AccountingNew", "BalanceSheet" },
                    { 58, null, null, false, null, null, 7, "AccountingNew", "Income" },
                    { 57, null, null, false, null, null, 7, "AccountingNew", "Liabilities" },
                    { 56, null, null, false, null, null, 7, "AccountingNew", "Assets" },
                    { 55, null, null, false, null, null, 6, "Marketing", "ActivityType" },
                    { 54, null, null, false, null, null, 6, "Marketing", "MediaCategory" },
                    { 53, null, null, false, null, null, 6, "Marketing", "Medium" },
                    { 52, null, null, false, null, null, 6, "Marketing", "Nature" },
                    { 51, null, null, false, null, null, 6, "Marketing", "Phase" },
                    { 50, null, null, false, null, null, 6, "Marketing", "Quality" },
                    { 59, null, null, false, null, null, 7, "AccountingNew", "Expense" },
                    { 71, null, null, false, null, null, 8, "Projects", "CriteriaEvaluation" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_ApproveRejectPermission_CreatedById",
                table: "ApproveRejectPermission",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ApproveRejectPermission_ModifiedById",
                table: "ApproveRejectPermission",
                column: "ModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_ApproveRejectPermission_PageId",
                table: "ApproveRejectPermission",
                column: "PageId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ApproveRejectPermission");

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 49);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 50);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 51);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 52);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 53);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 54);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 55);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 56);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 57);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 58);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 59);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 60);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 61);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 62);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 63);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 64);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 65);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 66);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 67);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 68);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 69);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 70);

            migrationBuilder.DeleteData(
                table: "ApplicationPages",
                keyColumn: "PageId",
                keyValue: 71);
        }
    }
}
