using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class EmployeePaymentTypesForeignKeyAdvanceId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "AdvanceId",
                table: "EmployeePaymentTypes",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AdvanceDetail",
                columns: table => new
                {
                    CreatedDate = table.Column<DateTime>(nullable: true),
                    ModifiedDate = table.Column<DateTime>(nullable: true),
                    CreatedById = table.Column<string>(nullable: true),
                    ModifiedById = table.Column<string>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: true),
                    AdvanceId = table.Column<long>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    CurrencyCode = table.Column<string>(maxLength: 10, nullable: true),
                    RegCode = table.Column<string>(maxLength: 10, nullable: true),
                    VoucherReferenceNo = table.Column<string>(maxLength: 10, nullable: true),
                    AdvanceDate = table.Column<DateTime>(nullable: true),
                    EmployeeId = table.Column<long>(nullable: true),
                    Description = table.Column<string>(maxLength: 200, nullable: true),
                    ApprovedBy = table.Column<string>(maxLength: 50, nullable: true),
                    ModeOfReturn = table.Column<string>(maxLength: 50, nullable: true),
                    RequestAmount = table.Column<float>(nullable: true),
                    AdvanceAmount = table.Column<float>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvanceDetail", x => x.AdvanceId);
                    table.ForeignKey(
                        name: "FK_AdvanceDetail_AspNetUsers_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvanceDetail_AspNetUsers_ModifiedById",
                        column: x => x.ModifiedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EmployeePaymentTypes_AdvanceId",
                table: "EmployeePaymentTypes",
                column: "AdvanceId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceDetail_CreatedById",
                table: "AdvanceDetail",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_AdvanceDetail_ModifiedById",
                table: "AdvanceDetail",
                column: "ModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeePaymentTypes_AdvanceDetail_AdvanceId",
                table: "EmployeePaymentTypes",
                column: "AdvanceId",
                principalTable: "AdvanceDetail",
                principalColumn: "AdvanceId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EmployeePaymentTypes_AdvanceDetail_AdvanceId",
                table: "EmployeePaymentTypes");

            migrationBuilder.DropTable(
                name: "AdvanceDetail");

            migrationBuilder.DropIndex(
                name: "IX_EmployeePaymentTypes_AdvanceId",
                table: "EmployeePaymentTypes");

            migrationBuilder.DropColumn(
                name: "AdvanceId",
                table: "EmployeePaymentTypes");
        }
    }
}
